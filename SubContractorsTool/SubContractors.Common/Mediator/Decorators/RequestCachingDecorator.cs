using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;
using StackExchange.Redis;
using SubContractors.Common.Redis;
using System;
using System.Data.SqlClient;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Common.Mediator.Decorators
{
    public class RequestCachingDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, Result<TResult>> where TRequest : IRequest<Result<TResult>>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ICachingOptions _cachingOptions;
        private readonly ILogger _logger;
        public const string responseMessage = "Operation was successful";
        public RequestCachingDecorator(IDistributedCache distributedCache,
                                       ICachingOptions cachingOptions,
                                       ILogger<RequestCachingDecorator<TRequest, TResult>> logger)
        {
            _distributedCache = distributedCache;
            _cachingOptions = cachingOptions;
            _logger = logger;
        }

        public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResult>> next)
        {
            var strategy = await CreatePolicyStrategy(next);

            var result = await strategy.ExecuteAndCaptureAsync(async (ct) =>
                                                            {
                                                              return  await GetSetCacheAsync(request, cancellationToken, next);
                                                            },
                                                            cancellationToken);

            return result.Outcome == OutcomeType.Successful ? result.Result : Result.Error<TResult>(result.FinalException);           
        }

        public async Task<Result<TResult>> GetSetCacheAsync(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResult>> next)
        {
            Result<TResult> handleResult = null;

            var cacheKey = ReflectObjectProperties(request);
            var cachedObject = await _distributedCache.GetStringAsync(cacheKey, token: cancellationToken);

            if (cachedObject != null)
            {
                var data = JsonSerializer.Deserialize<TResult>(cachedObject);
                return  Result.Ok(responseMessage, data);
            }

            handleResult = await next();

            if (handleResult.StatusCode == 200 && handleResult.IsSuccess)
            {
                await _distributedCache.SetStringAsync(cacheKey,
                                                       JsonSerializer.Serialize(handleResult.Data),
                                                       new DistributedCacheEntryOptions
                                                       {
                                                           AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cachingOptions.AbsoluteExpirationRelativeToNow),
                                                           SlidingExpiration = TimeSpan.FromMinutes(_cachingOptions.SlidingExpiration),
                                                       }, token: cancellationToken);
            }           

            return handleResult;
        }

        public static string ReflectObjectProperties(object Object)
        {
            var returnValue = string.Empty;            

            var objectType = Object.GetType();
            returnValue += objectType + " ";
            var properties = objectType.GetProperties();

            foreach (var property in properties)
            {
                returnValue += $"{property.Name}={property.GetValue(Object)}";
            }

            var method = objectType.GetMethod("GetDomainIdentifier");
            if (method is null)
            {
                throw new Exception($"Couldn't cache response result, should implement into command class type ICacheableRequest");
            }

            var methodResponse = method.Invoke(Object, null);
            returnValue += $" {methodResponse.ToString()}";

            return returnValue;
        }

        public async Task<AsyncPolicyWrap<Result<TResult>>> CreatePolicyStrategy(RequestHandlerDelegate<Result<TResult>> next)
        {
            var redisPolicy = Policy<Result<TResult>>
                .Handle<RedisConnectionException>()
                .Or<RedisCommandException>()
                .FallbackAsync( async (x) =>
                {
                    return await next();
                },
                ex =>
                {
                    _logger.LogError(ex.Exception.Message, ex.Exception);
                    return Task.CompletedTask;
                });

            var sqlPolicy = Policy<Result<TResult>>
                .Handle<SqlException>()
                .FallbackAsync(async x =>
                {
                    return await next();
                }, (ex) =>
                {
                    if (ex.Exception.InnerException != null)
                        throw ex.Exception.InnerException;

                    throw ex.Exception;
                });

            return Policy.WrapAsync(redisPolicy, sqlPolicy);
        }
    }
}
