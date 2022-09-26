using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubContractors.Application.Handlers.Invoices.Commands.CreateInvoice;
using SubContractors.Application.Handlers.Invoices.Commands.UpdateInvoice;
using SubContractors.Application.Handlers.Invoices.Commands.UpdateInvoiceStatus;
using SubContractors.Application.Handlers.Invoices.Commands.UploadSupportingDocuments;
using SubContractors.Application.Handlers.Invoices.Queries.ExportInvoicesQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetAllInvoicesPagedQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoicesQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceStatusesQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetMilestonesQuery;
using SubContractors.Application.Handlers.Invoices.Queries.GetReferralListQuery;
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
    public class InvoiceController : ServiceController
    {
        public InvoiceController(IDispatcher dispatcher) : base(dispatcher)
        { }

        [HttpGet]
        [SwaggerOperation("retrieve subcontractor invoices list from database")]
        [SwaggerResponse(200, "The list of invoices", typeof(SwaggerResultGet<IList<GetInvoicesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetInvoicesDto>>> Get([FromQuery] GetInvoicesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Pages/All")]
        [SwaggerOperation("retrieve invoices list from database")]
        [SwaggerResponse(200, "The list of invoices", typeof(SwaggerResultGet<PagedResult<GetAllInvoicesPagedDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<PagedResult<GetAllInvoicesPagedDto>>> Get([FromQuery] GetAllInvoicesPagedQuery query)
        {
            return await QueryAsync<Result<PagedResult<GetAllInvoicesPagedDto>>>(query);
        }

        [HttpGet("{Id}")]
        [SwaggerOperation("retrieve invoice from database")]
        [SwaggerResponse(200, "compliance with provided identifier", typeof(SwaggerResultGet<GetInvoiceDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<GetInvoiceDto>> Get([FromRoute] GetInvoiceQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Milestones")]
        [SwaggerOperation("retrieve milestones list from PM.API by passing projectId")]
        [SwaggerResponse(200, "The list of mailstones", typeof(SwaggerResultGet<IList<GetMilestoneDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<List<GetMilestoneDto>>> Get([FromQuery] GetMilestoneQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Referral")]
        [SwaggerOperation("retrieve referral list from database")]
        [SwaggerResponse(200, "The list of referrals", typeof(SwaggerResultGet<IList<GetReferralListDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<List<GetReferralListDto>>> Get([FromQuery] GetReferralListQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpPut]
        [SwaggerOperation("update invoice")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Put([FromBody] UpdateInvoice command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPut("Status")]
        [SwaggerOperation("update  invoice status")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<Unit>> Put([FromBody] UpdateInvoiceStatus command)
        {
            return await ExecuteAsync(command);
        }

        [HttpPost]
        [SwaggerOperation("create invoice")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<int>> Post([FromBody] CreateInvoice command)
        {
            return await ExecuteAsync(command);
        }
                
        [HttpPost("Files")]
        [SwaggerOperation("upload invoice files")]
        [SwaggerResponse(200, "Operation was successful", typeof(SwaggerResultPost<IList<UploadSupportingDocumentsDto>>))]
        [SwaggerResponse(400, "Operation was interrupted because of bad request", typeof(SwaggerResultException))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultPost))]
        [SwaggerResponse(415, "Validation error", typeof(SwaggerResultValidationFailure))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<UploadSupportingDocumentsDto>>> Post([FromQuery] UploadSupportingDocuments command)
        {
            return await ExecuteAsync(command);
        }

        [HttpGet("Export")]
        [SwaggerOperation("export invoice list")]
        [SwaggerResponse(200, "Csv file", typeof(SwaggerResultGet<ExportInvoicesDto>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<ExportInvoicesDto>> Get([FromQuery] ExportInvoicesQuery query)
        {
            return await QueryAsync(query);
        }

        [HttpGet("Status")]
        [SwaggerOperation("retrieve  invoice statuses from database")]
        [SwaggerResponse(200, "The list of active statuses", typeof(SwaggerResultGet<IList<GetInvoiceStatusesDto>>))]
        [SwaggerResponse(404, "Couldn't find related data", typeof(SwaggerResultGet<SwaggerEmptyJsonSample>))]
        [SwaggerResponse(500, "Interval server error", typeof(SwaggerResultException))]
        public async Task<Result<IList<GetInvoiceStatusesDto>>> Get([FromQuery] GetInvoiceStatusesQuery query)
        {
            return await QueryAsync(query);
        }
    }
}