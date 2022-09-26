using Microsoft.AspNetCore.Mvc;
using SubContractors.Application.Handlers.Common.Queries.GetCurrenciesQuery;
using SubContractors.Application.Handlers.Common.Queries.GetLocationsQuery;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ServiceController
    {
        public CommonController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet("Locations")]
        [SwaggerOperation("retrieve locations from database")]
        [SwaggerResponse(200, "The list of active locations", typeof(SwaggerResultGet<IList<GetLocationsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetLocationsDto>>> Get([FromQuery] GetLocationsQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Currencies")]
        [SwaggerOperation("retrieve currencies from database")]
        [SwaggerResponse(200, "The list of active currencies", typeof(SwaggerResultGet<IList<GetCurrencyDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetCurrencyDto>>> Get([FromQuery] GetCurrenciesQuery query)
        {
            return await QueryAsync(query);
        }
    }
}