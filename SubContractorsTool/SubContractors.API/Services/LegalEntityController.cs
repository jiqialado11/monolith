using Microsoft.AspNetCore.Mvc;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using SubContractors.Application.Handlers.LegalEntity.Queries.GetLegalEntitiesQuery;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegalEntityController : ServiceController
    {
        public LegalEntityController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet]
        [SwaggerOperation("retrieve  legal entities from database")]
        [SwaggerResponse(200, "The list of active legal entities", typeof(SwaggerResultGet<IList<GetLegalEntitiesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetLegalEntitiesDto>>> Get([FromQuery] GetLegalEntitiesQuery query)
        {
            return await QueryAsync(query);
        }
    }
}