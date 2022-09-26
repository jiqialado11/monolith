using MediatR;
using System.Threading.Tasks;

namespace SubContractors.Common.Mediator
{
    public class Dispatcher : IDispatcher
    {
        private readonly IMediator _mediator;

        public Dispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResult> QueryAsync<TResult>(IRequest<TResult> @query)
        {
            return await _mediator.Send(@query);
        }

        public async Task NotifyAsync<TEvent>(TEvent @event) where TEvent : INotification
        {
            await _mediator.Publish(@event);
        }

        public async Task<TResponse> RequestAsync<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }

        public Task Request<TResponse>(IRequest request)
        {
            return _mediator.Send(request);
        }
    }
}