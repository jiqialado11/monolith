using MediatR;
using SubContractors.Application.Handlers.Invoices.Queries.GetMilestonesQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Extensions;
using SubContractors.Common.Mediator;
using SubContractors.Domain.Invoice;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Commands.ApproveInvoice
{
    public class ApproveInvoiceHandler : IRequestHandler<ApproveInvoice, Result<Unit>>
    {
        private readonly IDispatcher _dispatcher;
        private readonly ISqlRepository<Invoice, int> _invoiceSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ApproveInvoiceHandler(ISqlRepository<Invoice, int> sqlRepository,
                                     IUnitOfWork unitOfWork,
                                     IDispatcher dispatcher)
        {
            _invoiceSqlRepository = sqlRepository;
            _dispatcher = dispatcher;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Unit>> Handle(ApproveInvoice request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceSqlRepository.GetAsync(x => x.Id == request.Id, new string [] { nameof(Invoice.Project) });
            if (invoice == null)
            {
                return Result.NotFound($"Invoice wasn't found in database with provided identifier {request.Id}");
            }

            if (invoice.InvoiceStatus == InvoiceStatus.Reviewed)
            {
                if (request.Milestone != null)
                {
                    var pmMilestones = await _dispatcher.RequestAsync(new GetMilestoneQuery
                                                                            {
                                                                                ProjectId = invoice.Project.PmId
                                                                            });
                    var pmMilestone = pmMilestones.Data?.FirstOrDefault(x => x.PmId == request.Milestone.PmId);
                    if (pmMilestone is null)
                    {
                        return Result.NotFound($"Milestone wasn't found in PmAccounting with provided identifier {request.Milestone.PmId}");
                    }

                    var milestone = new Milestone
                    {
                        Amount = request.Milestone.Amount,
                        Name = request.Milestone.Name,
                        PmId = request.Milestone.PmId,
                        ToDate = request.Milestone.ToDate,
                    };

                    invoice.AssignMilestone(milestone);
                }

                invoice.InvoiceStatus = (InvoiceStatus)request.InvoiceStatusId;

                await _invoiceSqlRepository.UpdateAsync(invoice);
                await _unitOfWork.SaveAsync();
                return Result.Ok();

                //Add when integration with budgets system will be ready
                //var registerRequest = new RegisterInvoices.RegisterInvoices { InvoiceNumber = request.InvoiceNumber };
                //return await _dispatcher.RequestAsync(registerRequest);
            }

            return Result.NotFound($"Invoice with status {invoice.InvoiceStatus.GetDescription()} couldn't approve");
        }
    }
}
