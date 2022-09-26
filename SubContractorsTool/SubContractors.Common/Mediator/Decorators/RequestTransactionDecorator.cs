using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubContractors.Common.EfCore.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Common.Mediator.Decorators
{
    public class RequestTransactionDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, Result<TResult>> where TRequest : IRequest<Result<TResult>>
    {
        private readonly ILogger<RequestTransactionDecorator<TRequest, TResult>> _logger;
        private readonly IUnitOfWork _uow;

        public RequestTransactionDecorator(ILogger<RequestTransactionDecorator<TRequest, TResult>> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public async Task<Result<TResult>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResult>> next)
        {
            await using var context = _uow.Context;
            if (context.Database.IsSqlServer())
            {
                await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var result = await next();
                    await transaction.CommitAsync(cancellationToken);
                    return result;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message, exception);
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }


            try
            {
                var result = await next();
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                throw;
            }
        }
    }
}