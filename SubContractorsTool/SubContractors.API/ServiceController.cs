using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubContractors.Common.Mediator;
using System.Threading.Tasks;
using SubContractors.Common;

namespace SubContractors.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ServiceController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            if (query == null)
            {
                var result = (TResult)Activator.CreateInstance(typeof(TResult), ResultType.BadRequest, "Request object is invalid");
                return result;
            }
            return await _dispatcher.QueryAsync(query);
        }

        protected async Task<TResult> ExecuteAsync<TResult>(IRequest<TResult> command)
        {
            if (command == null)
            {
                var result = (TResult)Activator.CreateInstance(typeof(TResult), ResultType.BadRequest, "Request object is invalid");
                return result;
            }
            return await _dispatcher.RequestAsync(command);
        }

        protected async Task Execute(IRequest command)
        {
            await _dispatcher.RequestAsync(command);
        }
        
    }
}