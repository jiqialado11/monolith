using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Extensions;
using SubContractors.Common.Mediator;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Invoice;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Commands.UpdateInvoiceStatus
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class UpdateInvoiceStatusHandler : IRequestHandler<UpdateInvoiceStatus, Result<Unit>>
    {
        private readonly ISqlRepository<Invoice, int> _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;

        public UpdateInvoiceStatusHandler(ISqlRepository<Invoice, int> sqlRepository,
            IUnitOfWork unitOfWork,
            IDispatcher dispatcher)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public async Task<Result<Unit>> Handle(UpdateInvoiceStatus request, CancellationToken cancellationToken)
        {
            if (request.StatusId == (int)InvoiceStatus.Approved)
            {
                var approveRequest = new ApproveInvoice.ApproveInvoice
                {
                    Id = request.InvoiceId,
                    InvoiceNumber = request.InvoiceNumber,
                    InvoiceStatusId = request.StatusId,
                    Milestone = request.Milestone
                };

                return await _dispatcher.RequestAsync(approveRequest);
            }

            var invoice = await _sqlRepository.GetAsync(x => x.Id == request.InvoiceId);
            if (invoice == null)
            {
                return Result.NotFound($"Invoice wasn't found in database with provided identifier {request.InvoiceId}");
            }

            if (!Enum.IsDefined(typeof(InvoiceStatus), request.StatusId))
            {
                return Result.NotFound($"Status wasn't found with provided identifier {request.StatusId}");
            }

            if (invoice.InvoiceStatus == InvoiceStatus.Approved || invoice.InvoiceStatus == InvoiceStatus.SentToPay)
            {
                return Result.NotFound($"Couldn't change invoice with status {invoice.InvoiceStatus.GetDescription()}" +
                    $" to status {((InvoiceStatus)request.StatusId).GetDescription()}");
            }

            if (invoice.InvoiceStatus == InvoiceStatus.Reviewed &&
                    (request.StatusId != (int)InvoiceStatus.Rejected || request.StatusId != (int)InvoiceStatus.Rejected))
            {
                return Result.NotFound($"Couldn't change invoice with status {invoice.InvoiceStatus.GetDescription()}" +
                    $" to status New");
            }

            if (invoice.InvoiceStatus == InvoiceStatus.Rejected &&
                    request.StatusId != (int)InvoiceStatus.New)
            {
                return Result.NotFound($"Couldn't change invoice with status {invoice.InvoiceStatus.GetDescription()}" +
                                 $" to status {((InvoiceStatus)request.StatusId).GetDescription()}");
            }

            if (invoice.InvoiceStatus == InvoiceStatus.New &&
                    request.StatusId != (int)InvoiceStatus.Reviewed && 
                    request.StatusId != (int)InvoiceStatus.Rejected)
            {
                return Result.NotFound($"Couldn't change invoice with status {invoice.InvoiceStatus.GetDescription()}" +
                                 $" to status {((InvoiceStatus)request.StatusId).GetDescription()}");
            }

            invoice.InvoiceStatus = (InvoiceStatus)request.StatusId;

            await _sqlRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }

        
    }
}
