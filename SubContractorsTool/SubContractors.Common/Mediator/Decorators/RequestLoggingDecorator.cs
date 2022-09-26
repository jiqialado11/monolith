using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Common.Mediator.Decorators
{
    public class RequestLoggingDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, Result<TResult>> where TRequest : IRequest<Result<TResult>>
    {
        private readonly ILogger<RequestLoggingDecorator<TRequest, TResult>> _logger;

        public RequestLoggingDecorator(ILogger<RequestLoggingDecorator<TRequest, TResult>> logger)
        {
            _logger = logger;
        }

        public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResult>> next)
        {
            Result<TResult> handleResult;

            var result = await Policy.Handle((Exception ex) =>
                                      {
                                          if (ex.InnerException != null)
                                          {
                                              _logger.LogError(ex.InnerException, $"{ex.Message}.");
                                          }

                                          _logger.LogError(ex, $"{ex.Message}. ");
                                          return true;
                                      })
                                     .RetryAsync(0)
                                     .ExecuteAndCaptureAsync(async () =>
                                      {
                                          handleResult = await next();
                                          return handleResult;
                                      });

            return result.Outcome == OutcomeType.Successful ? result.Result : Result.Error<TResult>(result.FinalException);
        }
    }
}