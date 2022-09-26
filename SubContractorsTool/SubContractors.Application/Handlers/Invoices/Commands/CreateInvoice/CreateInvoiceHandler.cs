using MediatR;
using SubContractors.Application.Common;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Invoice;
using SubContractors.Domain.SubContractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Commands.CreateInvoice
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoice, Result<int>>
    {
        private readonly ISqlRepository<Domain.SubContractor.SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.Invoice.Invoice, int> _invoiceSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly ISqlRepository<Domain.Agreement.Addendum, int> _addendumSqlRepository;
        private readonly ISqlRepository<Domain.Budget.PaymentTerm, int> _paymentTermSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly ISqlRepository<Domain.Invoice.SupportingDocument, Guid> _supportingDocumentSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateInvoiceHandler(ISqlRepository<Domain.SubContractor.SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Domain.Invoice.Invoice, int> invoiceSqlRepository,
            ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository,
            ISqlRepository<Domain.Agreement.Addendum, int> addendumSqlRepository,
            ISqlRepository<Domain.Budget.PaymentTerm, int> paymentTermSqlRepository,
            ISqlRepository<SupportingDocument, Guid> supportingDocumentSqlRepository,
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository,
            IUnitOfWork unitOfWork)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _invoiceSqlRepository = invoiceSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _addendumSqlRepository = addendumSqlRepository;
            _paymentTermSqlRepository = paymentTermSqlRepository;
            _supportingDocumentSqlRepository = supportingDocumentSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateInvoice request, CancellationToken cancellationToken)
        {
            var registeredInvoice = await _invoiceSqlRepository.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                                             x.InvoiceStatus == InvoiceStatus.SentToPay);
            if (registeredInvoice != null)
            {
                return Result.NotFound<int>($"Couldn't create invoice with invoice number {request.InvoiceNumber}");
            }

            var invoice = new Domain.Invoice.Invoice();
            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId);
            if (subContractor == null)
            {
                return Result.NotFound<int>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var addendum = await _addendumSqlRepository.GetAsync(x => x.Id == request.AddendumId);
            if (addendum == null)
            {
                return Result.NotFound<int>($"Addendum wasn't found in database with provided identifier {request.AddendumId}");
            }

            if (invoice.EndDate > addendum.EndDate)
            {
                return Result.NotFound<int>($"Couldn't create invoice with invoice end date {invoice.EndDate} which more than Addendum end date {addendum.EndDate}");
            }

            if (request.PaymentNumber != null &&
                    subContractor.SubContractorType != SubContractorType.HrPartner)
            {
                return Result.NotFound<int>($"Value for PaymentNumber must be equal to null, when SubContractorType isn't equal to HR Partner");
            }

            if (subContractor.SubContractorType == SubContractorType.HrPartner &&
                    request.PaymentNumber == null &&
                    request.ReferralId != null)
            {
                return Result.NotFound<int>($"Value for PaymentNumber must be not equal to null, when SubContractorType is equal to HR Partner");
            }
            
            if (subContractor.SubContractorType == SubContractorType.HrPartner && 
                    request.PaymentNumber != null &&
                    request.ReferralId == null)
            {
                return Result.NotFound<int>($"Value for ReferralId must be not equal to null, when SubContractorType is equal to HR Partner");
            }

            if (request.ReferralId != null &&
                    subContractor.SubContractorType != SubContractorType.HrPartner)
            {
                return Result.NotFound<int>("Value for ReferralId must be not equal to null, when SubContractorType is equal to HR Partner");
            }

            if (subContractor.SubContractorType == SubContractorType.HrPartner && request.ReferralId != null && request.PaymentNumber != null)
            {
                if (request.PaymentNumber != 2 && request.PaymentNumber != 1)
                {
                    return Result.NotFound<int>(Constants.ValidationErrors.Invoice_PaymentNumber_Value_Range);
                }

                var referral = await _staffSqlRepository.GetAsync(x => x.Id == request.ReferralId,
                    new string[] { nameof(Domain.SubContractor.Staff.Staff.Invoices) });
                if (referral == null)
                {
                    return Result.NotFound<int>($"Referral wasn't found in database with provided identifier {request.ReferralId}");
                }

                var referralInvoice = referral.Invoices
                                                  .Where(x => x.PaymentNumber == 1)
                                                  .FirstOrDefault();                              
                
                if (referralInvoice != null && request.PaymentNumber == 1)
                {
                    return Result.NotFound<int>($"Couldn't assign referral to this invoice with provided PaymentNumber {request.PaymentNumber}.");
                }

                if (request.PaymentNumber == 2 && referralInvoice != null)
                {
                    var referralInvoiceSecond = referral.Invoices
                                                  .Where(x => x.PaymentNumber == 2)
                                                  .FirstOrDefault();

                    if (referralInvoiceSecond != null)
                    {
                        return Result.NotFound<int>($"Couldn't create invoice with provided PaymentNumber {request.PaymentNumber}, " +
                                                    $"because we already have invoice with PaymentNumber {request.PaymentNumber}" +
                                                    $" for Referral {request.ReferralId}");
                    }
                }

                if (request.PaymentNumber == 2 && referralInvoice == null)
                {
                    return Result.NotFound<int>($"Couldn't assign referral to this invoice with provided PaymentNumber {request.PaymentNumber}." +
                                                $"Should create invoice with provided PaymentNumber = 1 for this Referral");
                }
              
                invoice.PaymentNumber = request.PaymentNumber;
                

                invoice.AssignReferral(referral);
            }

            var project = await _projectSqlRepository.GetAsync(x => x.Id == request.ProjectId,
                                                                new string [] 
                                                                {
                                                                    nameof(Domain.Project.Project.SubContractors),
                                                                    nameof(Domain.Project.Project.Addenda)
                                                                });
            if (project == null)
            {
                return Result.NotFound<int>($"Project wasn't found in database with provided identifier {request.ProjectId}");
            }
            var projectSubcontractor = project.SubContractors
                                            .Where(x => x.Id == request.SubContractorId)
                                            .FirstOrDefault();
            if (projectSubcontractor == null)
            {
                return Result.NotFound<int>($"Subcontractor with provided identifier {request.SubContractorId}" +
                                            $" doesn't contain project with provided identifier {request.ProjectId}");
            }

            var projectAddendum = project.Addenda
                                              .Where(x => x.Id == request.AddendumId)
                                              .FirstOrDefault();
            if (projectAddendum == null)
            {
                return Result.NotFound<int>($"Project with provided identifier {request.ProjectId}" +
                                            $" doesn't contain addendum with provided identifier {request.AddendumId}");
            }

            var invoiceFile = await _supportingDocumentSqlRepository.GetAsync(x => x.Id == request.InvoiceFileId);
            if (invoiceFile == null)
            {
                return Result.NotFound<int>($"Invoice File wasn't found in database with provided identifier {request.InvoiceFileId}");
            }

            var files = new List<SupportingDocument>();
            foreach (var fileId in request.SupportingDocumentsIds ?? new List<Guid>())
            {
                var file = await _supportingDocumentSqlRepository.GetAsync(x => x.Id == fileId);
                if (file == null)
                {
                    return Result.NotFound<int>($"Supporting Documentation File wasn't found in database with provided identifier {fileId}");
                }

                files.Add(file);
            }           

            if (request.TaxRate is null)
            {
                request.TaxRate = 0;
            }

            var taxAmount = request.Amount * request.TaxRate.Value * (decimal)0.01;
            
            invoice.CreateOrUpdateFields(request.Amount, request.StartDate, request.EndDate, request.InvoiceDate, request.InvoiceNumber, 
                request.TaxRate.Value, taxAmount, request.Comment, (int)InvoiceStatus.New, request.IsUseInvoiceDateForBudgetSystem);       

            invoice.AssignProject(project);
            invoice.AssignSubContractor(subContractor);
            invoice.AssignAddendum(addendum);
            invoice.AddInvoiceFile(invoiceFile);
            invoice.AddSupportingDocument(files);

            await _invoiceSqlRepository.AddAsync(invoice);
            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Created, data: invoice.Id);
        }
    }
}
