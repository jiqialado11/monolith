using System;
using Microsoft.AspNetCore.Mvc;
using SubContractors.Common;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Application.Handlers.Project.Commands.AddProject;
using SubContractors.Application.Handlers.Project.Commands.AssignInvoiceApprover;
using SubContractors.Application.Handlers.Project.Commands.DeAssignInvoiceApprover;
using SubContractors.Application.Handlers.Project.Commands.DeleteProject;
using SubContractors.Application.Handlers.Project.Queries.GetInternalProjectsListQuery;
using SubContractors.Application.Handlers.Project.Queries.GetPmProjectQuery;
using SubContractors.Application.Handlers.Project.Queries.GetProjectListQuery;
using SubContractors.Application.Handlers.Project.Queries.GetProjectQuery;
using SubContractors.Application.Handlers.Project.Queries.GetProjectStatusesQuery;
using SubContractors.Application.Handlers.Project.Queries.GetStaffProjectListQuery;
using SubContractors.Application.Handlers.Project.Queries.GetSubContractorsProjectListByStaffQuery;
using SubContractors.Application.Handlers.Project.Queries.SearchPmProjectQuery;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ServiceController
    {
        public ProjectController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet]
        [SwaggerOperation("retrieve internal list of all projects from database")]
        [SwaggerResponse(200, "The list of active projects", typeof(SwaggerResultGet<IList<GetInternalProjectsListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetInternalProjectsListDto>>> Get([FromQuery] GetInternalProjectsListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve project with identifier from database")]
        [SwaggerResponse(200, "retrieve project with identifier from database", typeof(SwaggerResultGet<GetProjectQueryDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetProjectQueryDto>> Get([FromRoute] GetProjectQuery query)
        {
            return await QueryAsync(query);
        }


        [HttpGet("PM")]
        [SwaggerOperation("retrieve project list from PM")]
        [SwaggerResponse(200, "The list of active project from PM", typeof(SwaggerResultGet<IList<SearchPmProjectDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<SearchPmProjectDto>>> Get([FromQuery] SearchPmProjectQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("PM/{Id}")]
        [SwaggerOperation("retrieve project with pm identifier from PM")]
        [SwaggerResponse(200, "retrieve project with pm identifier from PM", typeof(SwaggerResultGet<GetPmProjectDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetPmProjectDto>> Get([FromRoute] GetPmProjectQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost]
        [SwaggerOperation("assign project to subcontractor")]
        [SwaggerResponse(202, "Operation was successful, returns added project identifier", typeof(SwaggerResultPost<Guid>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Guid>> Post([FromBody] AddProject command)
        {
            return await ExecuteAsync(command);
        }

        
        [HttpGet("SubContractor/{SubContractorId}")]
        [SwaggerOperation("retrieve  projects from database by subcontractor identifier")]
        [SwaggerResponse(200, "The list of active projects", typeof(SwaggerResultGet<IList<GetProjectListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetProjectListDto>>> Get([FromRoute] GetProjectListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Staff/{StaffId}")]
        [SwaggerOperation("retrieve  projects from database by staff identifier")]
        [SwaggerResponse(200, "The list of active projects", typeof(SwaggerResultGet<IList<GetStaffProjectListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetStaffProjectListDto>>> Get([FromRoute] GetStaffProjectListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpDelete]
        [SwaggerOperation("delete project from subcontractor")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromQuery] DeleteProject command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("Status")]
        [SwaggerOperation("retrieve  project statuses from database")]
        [SwaggerResponse(200, "The list of project statuses", typeof(SwaggerResultGet<IList<GetProjectStatusesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetProjectStatusesDto>>> Get([FromQuery] GetProjectStatusesQuery query)
        {
            return await QueryAsync(query);
        }


        [HttpPost("Approve")]
        [SwaggerOperation("assign invoice approve to project")]
        [SwaggerResponse(202, "Operation was successful, returns added project identifier", typeof(SwaggerResultPost<Guid>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] AssignInvoiceApprover command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("Approve")]
        [SwaggerOperation("de-assign invoice approve to project")]
        [SwaggerResponse(202, "Operation was successful, returns added project identifier", typeof(SwaggerResultPost<Guid>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] DeAssignInvoiceApprover command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("SubContractors/Staff/{StaffId}")]
        [SwaggerOperation("retrieve  subcontractors projects from database by staff identifier")]
        [SwaggerResponse(200, "The list of active projects", typeof(SwaggerResultGet<IList<GetStaffProjectListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetSubContractorsProjectListByStaffDto>>> Get([FromRoute] GetSubContractorsProjectListByStaffQuery query)
        {
            return await QueryAsync(query);
        }
    }
}