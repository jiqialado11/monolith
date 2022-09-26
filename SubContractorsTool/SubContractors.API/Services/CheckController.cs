using Microsoft.AspNetCore.Mvc;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Application.Handlers.Check.Commands.CreateBackgroundCheck;
using SubContractors.Application.Handlers.Check.Commands.CreateSanctionCheck;
using SubContractors.Application.Handlers.Check.Commands.DeleteBackgroundCheck;
using SubContractors.Application.Handlers.Check.Commands.DeleteSanctionCheck;
using SubContractors.Application.Handlers.Check.Commands.UpdateBackgroundCheck;
using SubContractors.Application.Handlers.Check.Commands.UpdateSanctionCheck;
using SubContractors.Application.Handlers.Check.Queries.GetBackgroundCheckQuery;
using SubContractors.Application.Handlers.Check.Queries.GetBackgroundChecksQuery;
using SubContractors.Application.Handlers.Check.Queries.GetCheckStatusesQuery;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionCheckQuery;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ServiceController
    {
        public CheckController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet("SanctionChecks")]
        [SwaggerOperation("retrieve sanction check list from database", "ParentType must be specified, either SubContractor = 1 or Staff = 2")]
        [SwaggerResponse(200, "The list of subcontractors sanction checks", typeof(SwaggerResultGet<IList<GetSanctionChecksDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetSanctionChecksDto>>> Get([FromQuery] GetSanctionChecksQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("SanctionCheck/{Id}")]
        [SwaggerOperation("retrieve staff sanction check from database")]
        [SwaggerResponse(200, "Sanction check", typeof(SwaggerResultGet<GetSanctionCheckDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetSanctionCheckDto>> Get([FromRoute] GetSanctionCheckQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost("SanctionCheck")]
        [SwaggerOperation("Create new sanction check", "ParentType possible values: SubContractor - 1, Staff - 2")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateSanctionCheck command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("Status")]
        [SwaggerOperation("retrieve  check statuses from database")]
        [SwaggerResponse(200, "The list of active statuses", typeof(SwaggerResultGet<IList<GetCheckStatusesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetCheckStatusesDto>>> Get([FromQuery] GetCheckStatusesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPut("SanctionCheck")]
        [SwaggerOperation("Update existing sanction check", "ParentType possible values: SubContractor - 1, Staff - 2")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateSanctionCheck command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("SanctionCheck/{Id}")]
        [SwaggerOperation("delete sanction check")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteSanctionCheck command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("BackgroundChecks")]
        [SwaggerOperation("retrieve staff background check list from database")]
        [SwaggerResponse(200, "The list of subcontractors background checks", typeof(SwaggerResultGet<IList<GetBackgroundChecksDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetBackgroundChecksDto>>> Get([FromQuery] GetBackgroundChecksQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("BackgroundCheck/{Id}")]
        [SwaggerOperation("retrieve staff background check from database")]
        [SwaggerResponse(200, "Background check", typeof(SwaggerResultGet<GetBackgroundCheckDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetBackgroundCheckDto>> Get([FromRoute] GetBackgroundCheckQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost("BackgroundCheck")]
        [SwaggerOperation("Create new background check")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateBackgroundCheck command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut("BackgroundCheck")]
        [SwaggerOperation("Update existing background check")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateBackgroundCheck command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("BackgroundCheck/{Id}")]
        [SwaggerOperation("delete background check")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteBackgroundCheck command)
        {
            return await ExecuteAsync(command);
        }
    }
}