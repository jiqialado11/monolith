using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Extensions;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Invoice;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Commands.UpdateInvoice
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoice, Result<Unit>>
    {
        private readonly IInvoiceSqlRepository _invoiceSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly ISqlRepository<Domain.Agreement.Addendum, int> _addendumSqlRepository;
        private readonly ISqlRepository<Domain.Invoice.SupportingDocument, Guid> _supportingDocumentSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInvoiceHandler(IInvoiceSqlRepository invoiceSqlRepository,
            ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository,
            ISqlRepository<Domain.Agreement.Addendum, int> addendumSqlRepository,
            ISqlRepository<Domain.Budget.PaymentTerm, int> paymentTermSqlRepository,
            ISqlRepository<SupportingDocument, Guid> supportingDocumentSqlRepository,
            IUnitOfWork unitOfWork)
        {
            _invoiceSqlRepository = invoiceSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _addendumSqlRepository = addendumSqlRepository;
            _supportingDocumentSqlRepository = supportingDocumentSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateInvoice request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceSqlRepository.GetAsync(x => x.Id == request.Id.Value);
            if (invoice == null)
            {
                return Result.NotFound($"Invoice wasn't found in database with provided identifier {request.Id}");
            }

            if (invoice.InvoiceNumber != request.InvoiceNumber)
            {
                var registeredInvoice = await _invoiceSqlRepository.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                                             x.InvoiceStatus == InvoiceStatus.SentToPay);
                if (registeredInvoice != null)
                {
                    return Result.NotFound<Unit>($"Couldn't update invoice with invoice number {request.InvoiceNumber}");
                }
            }

            if (invoice.InvoiceStatus != InvoiceStatus.New && invoice.InvoiceStatus != InvoiceStatus.Rejected)
            {
                return Result.NotFound($"Invoice with status {invoice.InvoiceStatus.GetDescription()}");
            }

            var project = await _projectSqlRepository.GetAsync(x => x.Id == request.ProjectId, new string[]
                                                                    {
                                                                        nameof(Domain.Project.Project.SubContractors),
                                                                        nameof(Domain.Project.Project.Addenda)
                                                                    });
            if (invoice.Project == null || invoice.Project.Id != request.ProjectId)
            {
                if (project == null)
                {
                    return Result.NotFound(
                                        $"Project wasn't found in database with provided identifier {request.ProjectId}");
                }

                var projectSubcontractor = project.SubContractors
                                            .Where(x => x.Id == invoice.SubContractor.Id)
                                            .FirstOrDefault();
                if (projectSubcontractor == null)
                {
                    return Result.NotFound($"Couldn't assign project with provided identifier {request.ProjectId} to current invoice");
                }

                invoice.AssignProject(project);
            }

            if (invoice.Addendum == null || invoice.Addendum.Id != request.AddendumId)
            {
                var addendum = await _addendumSqlRepository.GetAsync(x => x.Id == request.AddendumId);
                if (addendum == null)
                {
                    return Result.NotFound(
                                        $"Addendum wasn't found in database with provided identifier {request.AddendumId}");
                }
                                
                var projectAddendum = project.Addenda
                                              .Where(x => x.Id == request.AddendumId)
                                              .FirstOrDefault();
                if (projectAddendum == null)
                {
                    return Result.NotFound($"Project with provided identifier {request.ProjectId}" +
                                           $" doesn't contain addendum with provided identifier {request.AddendumId}");
                }

                invoice.AssignAddendum(addendum);
            }

            if (invoice.EndDate != request.EndDate)
            {
                if (invoice.EndDate > invoice.Addendum.EndDate)
                {
                    return Result.NotFound<Unit>($"Couldn't create invoice with invoice end date {invoice.EndDate} which more than Addendum end date {invoice.Addendum.EndDate}");
                }
            }

            if (invoice.InvoiceFile == null || invoice.InvoiceFile.Id != request.InvoiceFileId)
            {
                var invoiceFile = await _supportingDocumentSqlRepository.GetAsync(x => x.Id == request.InvoiceFileId);
                if (invoiceFile == null)
                {
                    return Result.NotFound(
                                        $"Invoice File wasn't found in database with provided identifier {request.InvoiceFileId}");
                }

                invoice.AddInvoiceFile(invoiceFile);
            }

            if (request.TaxRate is null)
            {
                request.TaxRate = 0;
            }

            var taxAmount = request.Amount * request.TaxRate.Value * (decimal)0.01;
                        
            invoice.CreateOrUpdateFields(request.Amount, request.StartDate, request.EndDate, request.InvoiceDate, request.InvoiceNumber,
                request.TaxRate.Value, taxAmount, request.Comment, null, request.IsUseInvoiceDateForBudgetSystem);

            var result = await UpdateSupportingDocumentationAsync(invoice, request.SupportingDocumentsIds);
            if (!string.IsNullOrEmpty(result))
            {
                return Result.NotFound(result);
            }

            await _invoiceSqlRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveAsync();

            return Result.Accepted();
        }

        private async Task<string> UpdateSupportingDocumentationAsync(Invoice invoice, IList<Guid> updateDocumentsIds)
        {
            var filesIds = new List<Guid>();

            if (invoice.SupportingDocuments is null && updateDocumentsIds.Count > 0)
            {
                invoice.SupportingDocuments = new List<SupportingDocument>();
                filesIds = updateDocumentsIds.ToList();
            }

            if (invoice.SupportingDocuments != null)
            {
                if (updateDocumentsIds.Any() && invoice.SupportingDocuments.Any())
                {
                    var allIdCollection = invoice.SupportingDocuments.Select(x => x.Id)
                                                                       .ToList();
                    allIdCollection.AddRange(updateDocumentsIds);
                    var res = allIdCollection.Distinct().ToList();
                    foreach (var inputId in res)
                    {
                        if (updateDocumentsIds.Contains(inputId))
                        {
                            filesIds.Add(inputId);
                        }
                        else
                        {
                            invoice.SupportingDocuments.Remove(invoice.SupportingDocuments.FirstOrDefault(x => x.Id == inputId));
                        }
                    }
                }
                else if (!invoice.SupportingDocuments.Any())
                {
                    filesIds = updateDocumentsIds.ToList();
                }
                else
                {
                    invoice.SupportingDocuments.Clear();
                    return null;
                }
            }

            for (int i = 0; i < filesIds.Count; i++)
            {
                if (invoice.SupportingDocuments.Select(x => x.Id).Contains(filesIds[i]))
                {
                    continue;
                }

                var file = await _supportingDocumentSqlRepository.GetAsync(x => x.Id == filesIds[i]);
                if (file == null)
                {
                    return $"Supporting Documentation File wasn't found in database with provided identifier {filesIds[i]}";
                }

                invoice.SupportingDocuments.Add(file);
            }

            return null;
        }
    }

}
