using Microsoft.AspNetCore.Mvc;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Application.Handlers.Compliance.Commands.CreateCompliance;
using SubContractors.Application.Handlers.Compliance.Commands.DeleteCompliance;
using SubContractors.Application.Handlers.Compliance.Commands.UpdateCompliance;
using SubContractors.Application.Handlers.Compliance.Commands.UploadComplianceFile;
using SubContractors.Application.Handlers.Compliance.Queries.DownloadComplianceFileQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceRatingsQuery;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceTypesQuery;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplianceController : ServiceController
    {
        public ComplianceController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("File")]
        [SwaggerOperation("upload compliance file")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost<IList<UploadComplianceFileDto>>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<UploadComplianceFileDto>> Post([FromQuery] UploadComplianceFile command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet]
        [SwaggerOperation("retrieve subcontractors compliance list from database")]
        [SwaggerResponse(200, "The list of subcontractors compliance", typeof(SwaggerResultGet<IList<GetComplianceDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetComplianceDto>>> Get([FromQuery] GetComplianceListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve compliance from database")]
        [SwaggerResponse(200, "compliance with provided identifier", typeof(SwaggerResultGet<GetComplianceDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetComplianceDto>> Get([FromRoute] GetComplianceQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost]
        [SwaggerOperation("create  compliance")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateCompliance command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut]
        [SwaggerOperation("update  compliance")]
        [SwaggerResponse(202, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateCompliance command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("{Id}")]
        [SwaggerOperation("delete compliance")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteCompliance command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("Ratings")]
        [SwaggerOperation("retrieve compliance ratings from database")]
        [SwaggerResponse(200, "The list of compliance ratings", typeof(SwaggerResultGet<IList<GetComplianceRatingsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetComplianceRatingsDto>>> Get([FromQuery] GetComplianceRatingsQuery query)
        {
            return await QueryAsync(query);
        }


        [HttpGet("Types")]
        [SwaggerOperation("retrieve compliance types from database")]
        [SwaggerResponse(200, "compliance types with provided identifier", typeof(SwaggerResultGet<GetComplianceTypeDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetComplianceTypeDto>>> Get([FromRoute] GetComplianceTypesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("File/{Id}")]
        [SwaggerOperation("download compliance file")]
        [SwaggerResponse(200, "compliance file with provided identifier", typeof(SwaggerResultGet<DownloadComplianceFileDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<DownloadComplianceFileDto>> Get([FromRoute] DownloadComplianceFileQuery query)
        {
            return await QueryAsync(query);
        }


    }
}