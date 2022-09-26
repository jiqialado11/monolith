using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubContractors.Application.Handlers.Project.Commands.DeleteStaffProject;
using SubContractors.Application.Handlers.Staff.Commands.AssignProject;
using SubContractors.Application.Handlers.Staff.Commands.CreateStaff;
using SubContractors.Application.Handlers.Staff.Commands.UpdateStaff;
using SubContractors.Application.Handlers.Staff.Queries.GetInternalStaffListQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetPmStaffQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetRateUnitsQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetStaffListQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetStaffQuery;
using SubContractors.Application.Handlers.Staff.Queries.GetStaffStatusesQuery;
using SubContractors.Application.Handlers.Staff.Queries.SearchPmStaffQuery;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ServiceController
    {
        public StaffController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet]
        [SwaggerOperation("retrieve staff list from database","Either by passing subContractorId to retrieve subContractor's staff " +
                                                              "or not passing SubContractorId to retrieve all staff records")]
        [SwaggerResponse(200, "The list of active staff", typeof(SwaggerResultGet<IList<GetStaffListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetStaffListDto>>> Get([FromQuery] GetStaffListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("List")]
        [SwaggerOperation("retrieve staff internal list from database")]
        [SwaggerResponse(200, "The list of active staff from database", typeof(SwaggerResultGet<IList<GetInternalStaffListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetInternalStaffListDto>>> Get([FromQuery] GetInternalStaffListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("RateUnits")]
        [SwaggerOperation("retrieve rate units from database")]
        [SwaggerResponse(200, "The list of active rate units", typeof(SwaggerResultGet<IList<GetRateUnitDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetRateUnitDto>>> Get([FromQuery] GetRateUnitsQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("PM")]
        [SwaggerOperation("retrieve staff list from PM")]
        [SwaggerResponse(200, "The list of active staff from PM", typeof(SwaggerResultGet<IList<SearchPmStaffDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<SearchPmStaffDto>>> Get([FromQuery] SearchPmStaffQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("PM/{Id}")]
        [SwaggerOperation("retrieve staff with pm identifier from PM")]
        [SwaggerResponse(200, "retrieve staff with pm identifier from PM", typeof(SwaggerResultGet<GetPmStaffDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetPmStaffDto>> Get([FromRoute] GetPmStaffQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve staff with with identifier")]
        [SwaggerResponse(200, "retrieve staff with identifier", typeof(SwaggerResultGet<GetStaffDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetStaffDto>> Get([FromRoute] GetStaffQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost]
        [SwaggerOperation("create  staff")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateStaff command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut]
        [SwaggerOperation("update  staff")]
        [SwaggerResponse(202, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateStaff command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPost("Project")]
        [SwaggerOperation("Assign project to staff")]
        [SwaggerResponse(202, "Operation was successful", typeof(SwaggerResultPost<Unit>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] AssignProject command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("Status")]
        [SwaggerOperation("retrieve  staff statuses from database")]
        [SwaggerResponse(200, "The list of staff statuses", typeof(SwaggerResultGet<IList<GetStaffStatusesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetStaffStatusesDto>>> Get([FromQuery] GetStaffStatusesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpDelete("Project")]
        [SwaggerOperation("delete project from staff")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromQuery] DeleteStaffProject command)
        {
            return await ExecuteAsync(command);
        }

    }
}