using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubContractors.Application.Handlers.Agreement.Commands.AddRate;
using SubContractors.Application.Handlers.Agreement.Commands.CreateAddendum;
using SubContractors.Application.Handlers.Agreement.Commands.CreateAgreement;
using SubContractors.Application.Handlers.Agreement.Commands.Delete_Addendum;
using SubContractors.Application.Handlers.Agreement.Commands.DeleteAgreement;
using SubContractors.Application.Handlers.Agreement.Commands.DeleteRate;
using SubContractors.Application.Handlers.Agreement.Commands.UpdateAddendum;
using SubContractors.Application.Handlers.Agreement.Commands.UpdateAgreement;
using SubContractors.Application.Handlers.Agreement.Commands.UpdateRate;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendaQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAgreementQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAgreementsQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetRateQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementController : ServiceController
    {
        public AgreementController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpPost]
        [SwaggerOperation("create  agreement")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of crated entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateAgreement command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut]
        [SwaggerOperation("update  agreement")]
        [SwaggerResponse(202, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateAgreement command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation("delete agreement")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteAgreement command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve  agreement from database")]
        [SwaggerResponse(200, "agreement with provided identifier", typeof(SwaggerResultGet<GetAgreementDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetAgreementDto>> Get([FromRoute] GetAgreementQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost("Addendum")]
        [SwaggerOperation("create  addendum")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateAddendum command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut("Addendum")]
        [SwaggerOperation("create  addendum")]
        [SwaggerResponse(202, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateAddendum command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("Addendum/{Id}")]
        [SwaggerOperation("delete addendum")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteAddendum command)
        {
            return await ExecuteAsync(command);
        }


        [HttpPost("Addendum/Rate")]
        [SwaggerOperation("Add rate for addendum")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] AddRate command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut("Addendum/Rate")]
        [SwaggerOperation("Add rate for addendum")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateRate command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("Addendum/Rate/{Id}")]
        [SwaggerOperation("delete rate")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteRate command)
        {
            return await ExecuteAsync(command);
        }


        [HttpGet("Addendum/{AddendumId}/Rates")]
        [SwaggerOperation("retrieve addendum rates from database")]
        [SwaggerResponse(200, "return addendum rates with provided identifier", typeof(SwaggerResultGet<IList<GetRatesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetRatesDto>>> Get([FromRoute] GetRatesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Addendum/{AddendumId}/Rates/{RateId}")]
        [SwaggerOperation("retrieve addendum rates from database")]
        [SwaggerResponse(200, "return addendum rates with provided identifier", typeof(SwaggerResultGet<GetRateDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetRateDto>> Get([FromRoute] GetRateQuery query)
        {
            return await QueryAsync(query);
        }




        [HttpGet("Addendum/{Id}")]
        [SwaggerOperation("retrieve  addendum from database")]
        [SwaggerResponse(200, "addendum with provided identifier", typeof(SwaggerResultGet<GetAddendumDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetAddendumDto>> Get([FromRoute] GetAddendumQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Addenda/{SubContractorId}")]
        [SwaggerOperation("retrieve subcontractors addenda from database")]
        [SwaggerResponse(200, "subcontractors addenda with provided identifier", typeof(SwaggerResultGet<IList<GetAddendumDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetAddendumDto>>> Get([FromRoute] GetAddendaQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Agreements/{SubContractorId}")]
        [SwaggerOperation("retrieve  subcontractor agreements from database")]
        [SwaggerResponse(200, "subcontractors agreements with provided identifier", typeof(SwaggerResultGet<IList<GetAgreementsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetAgreementsDto>>> Get([FromRoute] GetAgreementsQuery query)
        {
            return await QueryAsync(query);
        }

    }
}
