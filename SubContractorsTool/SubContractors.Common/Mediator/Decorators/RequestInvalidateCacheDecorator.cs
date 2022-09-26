using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Polly;
using StackExchange.Redis;
using SubContractors.Common.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Common.Mediator.Decorators
{
    public class RequestInvalidateCacheDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, Result<TResult>> where TRequest : IRequest<Result<TResult>>
    {
        private readonly ILogger _logger;
        private readonly IDistributedCache _distributedCache;
        private readonly ICachingOptions _cachingOptions;


        public RequestInvalidateCacheDecorator(ILogger<RequestInvalidateCacheDecorator<TRequest, TResult>> logger,
                                                  IDistributedCache distributedCache,
                                                  ICachingOptions cachingOptions)
        {
            _logger = logger;
            _distributedCache = distributedCache;
            _cachingOptions = cachingOptions;
        }

        public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResult>> next)
        {
            var objectType = request.GetType();
            var method = objectType.GetMethod("GetDomainsIdentifiers");
            if (method is null)
            {
                throw new Exception($"Couldn't invalidate cache because corresponding interface is missing, command needs to implement interface IInvalidateCacheResponse");
            }

            var methodResponse = (List<string>)method.Invoke(request, null);

            var result = await Policy<Result<TResult>>
                .Handle<RedisConnectionException>()
                .Or<RedisCommandException>()
                .FallbackAsync(async (x) =>
                {
                    return await next();
                },
                ex =>
                {
                    _logger.LogError(ex.Exception.Message, ex.Exception);
                    return Task.CompletedTask;
                }).ExecuteAndCaptureAsync(async (ct) =>
                {
                    var redisConnection = ConnectionMultiplexer.Connect(_cachingOptions.ConnectionString);
                    var redisKeys = redisConnection.GetServer(_cachingOptions.ConnectionString).Keys().ToList();

                    if (redisKeys.Count > 0)
                    {

                        foreach (var strKey in methodResponse.SelectMany(searchKey => redisKeys.Select(key => key.ToString())
                                                             .Where(strKey => strKey.Contains(searchKey))))
                        {
                            await _distributedCache.RemoveAsync(strKey.Substring(9), cancellationToken);
                        }
                    }                    

                    return await next();
                }, cancellationToken);

            return result.Outcome == OutcomeType.Successful ? result.Result : Result.Error<TResult>(result.FinalException);
        }
    }
}
