using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubContractors.Application.Handlers.Staff.Commands.AddStaff;
using SubContractors.Application.Handlers.Staff.Commands.DeleteStaff;
using SubContractors.Application.Handlers.SubContractors.Commands.CreateSubContractor;
using SubContractors.Application.Handlers.SubContractors.Commands.CreateTax;
using SubContractors.Application.Handlers.SubContractors.Commands.DeleteTax;
using SubContractors.Application.Handlers.SubContractors.Commands.UpdateSubContractor;
using SubContractors.Application.Handlers.SubContractors.Commands.UpdateSubContractorStatus;
using SubContractors.Application.Handlers.SubContractors.Commands.UpdateTax;
using SubContractors.Application.Handlers.SubContractors.Queries.GetMarketsQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetOfficesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractor;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsPagedQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsStatusesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsTypeQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxesQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxQuery;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxTypesQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Common.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubContractors.API.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubContractorController : ServiceController
    {
        public SubContractorController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet]
        [SwaggerOperation("retrieve subcontractors from database", "QueryType possible values are 0 = List, 1 = Library")]
        [SwaggerResponse(200, "The list of active subcontractors", typeof(SwaggerResultGet<PagedResult<GetSubContractorsPagedDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<PagedResult<GetSubContractorsPagedDto>>> Get([FromQuery] GetSubContractorsPagedQuery query)
        {
            return await QueryAsync<Result<PagedResult<GetSubContractorsPagedDto>>>(query);
        }

        [HttpGet("All")]
        [SwaggerOperation("retrieve  subcontractor identifiers and names from database")]
        [SwaggerResponse(200, "The list of active subcontractors", typeof(SwaggerResultGet<IList<GetSubContractorsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetSubContractorsDto>>> Get([FromQuery] GetSubContractorsQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve subcontractor from database")]
        [SwaggerResponse(200, "subcontractors with provided identifier", typeof(SwaggerResultGet<GetSubContractorDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetSubContractorDto>> Get([FromRoute] GetSubContractorQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Types")]
        [SwaggerOperation("retrieve subcontractor types from database")]
        [SwaggerResponse(200, "The list of active subcontractor types", typeof(SwaggerResultGet<IList<GetSubContractorTypesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetSubContractorTypesDto>>> Get([FromQuery] GetSubContractorsTypeQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Offices")]
        [SwaggerOperation("retrieve  subcontractors offices from database", "OfficeType possible values are 1 = sales office, 2 = development office")]
        [SwaggerResponse(200, "The list of active offices", typeof(SwaggerResultGet<IList<GetOfficesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetOfficesDto>>> Get([FromQuery] GetOfficesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Markets")]
        [SwaggerOperation("retrieve  subcontractors markets from database")]
        [SwaggerResponse(200, "The list of active offices", typeof(SwaggerResultGet<IList<GetMarketsDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetMarketsDto>>> Get([FromQuery] GetMarketsQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Status")]
        [SwaggerOperation("retrieve  subcontractors statuses from database")]
        [SwaggerResponse(200, "The list of active statuses", typeof(SwaggerResultGet<IList<GetSubContractorsStatusesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetSubContractorsStatusesDto>>> Get([FromQuery] GetSubContractorsStatusesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPut("Status")]
        [SwaggerOperation("update  subcontractor status")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateSubContractorStatus command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPost]
        [SwaggerOperation("create  subcontractor")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateSubContractor command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut]
        [SwaggerOperation("update  subcontractor")]
        [SwaggerResponse(202, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateSubContractor command)
        {
            return await ExecuteAsync(command);
        }


        [HttpPost("Staff")]
        [SwaggerOperation("Assign existing staff to subcontractor")]
        [SwaggerResponse(202, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] AddStaff command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("Staff")]
        [SwaggerOperation("delete staff from subcontractor")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromQuery] DeleteStaff command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("{SubContractorId}/Taxes")]
        [SwaggerOperation("retrieve subcontractors taxes from database")]
        [SwaggerResponse(200, "subcontractors taxes with provided identifier", typeof(SwaggerResultGet<IList<GetTaxesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetTaxesDto>>> Get([FromRoute] GetTaxesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Tax/{Id}")]
        [SwaggerOperation("retrieve tax from database")]
        [SwaggerResponse(200, "tax with provided identifier", typeof(SwaggerResultGet<GetTaxDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetTaxDto>> Get([FromRoute] GetTaxQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPost("Tax")]
        [SwaggerOperation("create  tax")]
        [SwaggerResponse(201, "Operation was successful, returns identifier of created entity", typeof(SwaggerResultPost<int>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateTax command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut("Tax")]
        [SwaggerOperation("update  tax")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromBody] UpdateTax command)
        {
            return await ExecuteAsync(command);
        }

        [HttpDelete("Tax/{Id}")]
        [SwaggerOperation("delete tax")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Post([FromRoute] DeleteTax command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("TaxType")]
        [SwaggerOperation("retrieve  tax types terms from database")]
        [SwaggerResponse(200, "The list of tax types", typeof(SwaggerResultGet<IList<GetTaxTypeDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetTaxTypeDto>>> Get([FromQuery] GetTaxTypesQuery query)
        {
            return await QueryAsync(query);
        }
    }
}