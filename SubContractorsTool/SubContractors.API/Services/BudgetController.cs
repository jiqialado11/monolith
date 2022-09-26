using Microsoft.AspNetCore.Mvc;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubContractors.Application.Handlers.Budget.Queries.GetPaymentMethodsQuery;
using SubContractors.Application.Handlers.Budget.Queries.GetPaymentTermsQuery;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ServiceController
    {
        public BudgetController(IDispatcher dispatcher) : base(dispatcher)
        { }
        
        [HttpGet("PaymentTerms")]
        [SwaggerOperation("retrieve  payment terms from database")]
        [SwaggerResponse(200, "The list of payment terms", typeof(SwaggerResultGet<IList<GetPaymentTermsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetPaymentTermsDto>>> Get([FromQuery] GetPaymentTermsQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("PaymentMethods")]
        [SwaggerOperation("retrieve  payment methods from database")]
        [SwaggerResponse(200, "The list of payment methods", typeof(SwaggerResultGet<IList<GetPaymentMethodsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetPaymentMethodsDto>>> Get([FromQuery] GetPaymentMethodQuery query)
        {
            return await QueryAsync(query);
        }
    }
}