using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Invoices.Commands.CreateInvoice;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Invoice;
using SubContractors.Domain.SubContractor;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractor.Tests.Handlers.Invoice
{
    internal class CreateInvoiceHandlerTests : BaseTestFixture
    {
        private IRequestHandler<CreateInvoice, Result<int>> _handler;

        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>> _subContractorSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.Invoice.Invoice, int>> _invoiceSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.Project.Project, Guid>> _projectSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.Agreement.Addendum, int>> _addendumSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.Budget.PaymentTerm, int>> _paymentTermSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.Invoice.SupportingDocument, Guid>> _supportingDocumentSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.Staff.Staff, int>> _staffSqlRepositoryMock;
        
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private CreateInvoiceValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new CreateInvoiceValidator();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();

            _subContractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _invoiceSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.Invoice.Invoice, int>>();
            _projectSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.Project.Project, Guid>>();
            _addendumSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.Agreement.Addendum, int>>();
            _paymentTermSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.Budget.PaymentTerm, int>>();
            _supportingDocumentSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.Invoice.SupportingDocument, Guid>>();
            _staffSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.Staff.Staff, int>>();

            _handler = new CreateInvoiceHandler(_subContractorSqlRepositoryMock.Object, _invoiceSqlRepositoryMock.Object,
                _projectSqlRepositoryMock.Object, _addendumSqlRepositoryMock.Object, _paymentTermSqlRepositoryMock.Object,
                _supportingDocumentSqlRepositoryMock.Object, _staffSqlRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test(Author = "Boris Artamonov", Description = "Creates new invoice")]
        public async Task Create_New_Invoice()
        {
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            project.SubContractors = new List<SubContractors.Domain.SubContractor.SubContractor>();
            project.Addenda = new List<SubContractors.Domain.Agreement.Addendum>();
            project.SubContractors.Add(subcontractor);
            project.Addenda.Add(addendum);
            
            var paymentTerm = new SubContractors.Domain.Budget.PaymentTerm(_fixture.Create<int>());
            var milestone = new SubContractors.Domain.Invoice.Milestone(_fixture.Create<int>());
            var invoiceFile = new SubContractors.Domain.Invoice.SupportingDocument(_fixture.Create<Guid>());

            var request = new CreateInvoice
            {
                AddendumId = addendum.Id,
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = invoiceFile.Id,
                InvoiceNumber = _fixture.Create<string>(),
                ProjectId = project.Id,
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(addendum)
                .Verifiable();

            _projectSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.ProjectId, new string[]
                                            {
                                                nameof(SubContractors.Domain.Project.Project.SubContractors),
                                                nameof(SubContractors.Domain.Project.Project.Addenda)
                                            }))
                .ReturnsAsync(project)
                .Verifiable();

            _supportingDocumentSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.InvoiceFileId, null))
                .ReturnsAsync(invoiceFile)
                .Verifiable();

            _invoiceSqlRepositoryMock.Setup(x => x.AddAsync(It.IsAny<SubContractors.Domain.Invoice.Invoice>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);

            Expression<Func<SubContractors.Domain.Invoice.Invoice, bool>> match = f =>
                f.SubContractor.Id == request.SubContractorId 
                && f.Addendum.Id == request.AddendumId
                && f.Amount == request.Amount
                && f.Comment == request.Comment
                && f.EndDate == request.EndDate
                && f.InvoiceDate == request.InvoiceDate
                && f.InvoiceFile.Id == request.InvoiceFileId
                && f.InvoiceNumber == request.InvoiceNumber
                && f.PaymentNumber == request.PaymentNumber
                && f.Project.Id == request.ProjectId
                && f.StartDate == request.StartDate
                && f.IsUseInvoiceDateForBudget == request.IsUseInvoiceDateForBudgetSystem
                && f.TaxRate == request.TaxRate;

            _invoiceSqlRepositoryMock.Verify(f => f.AddAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Boris Artamonov", Description = "subcontractor not found")]
        public async Task SubContractor_Not_Found()
        {
            var request = new CreateInvoice
            {
                AddendumId = _fixture.Create<int>(),
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = _fixture.Create<string>(),
                PaymentNumber = _fixture.Create<int>(),
                ProjectId = _fixture.Create<Guid>(),
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = _fixture.Create<int>(),
                TaxRate = _fixture.Create<decimal>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Boris Artamonov", Description = "project not found")]
        public async Task Project_Not_Found()
        {
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            project.Addenda = new List<SubContractors.Domain.Agreement.Addendum>();
            project.SubContractors = new List<SubContractors.Domain.SubContractor.SubContractor>();
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            project.Addenda.Add(addendum);
            project.SubContractors.Add(subcontractor);

            var request = new CreateInvoice
            {
                AddendumId = _fixture.Create<int>(),
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = _fixture.Create<string>(),
                ProjectId = _fixture.Create<Guid>(),
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(addendum)
                .Verifiable();

            _projectSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.ProjectId, new string[]
                                            {
                                                nameof(SubContractors.Domain.Project.Project.SubContractors),
                                                nameof(SubContractors.Domain.Project.Project.Addenda)
                                            }))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Boris Artamonov", Description = "addendum not found")]
        public async Task Addendum_Not_Found()
        {

            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            project.Addenda = new List<SubContractors.Domain.Agreement.Addendum>();
            project.SubContractors = new List<SubContractors.Domain.SubContractor.SubContractor>();
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            project.Addenda.Add(addendum);
            project.SubContractors.Add(subcontractor);            

            var request = new CreateInvoice
            {
                AddendumId = _fixture.Create<int>(),
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = _fixture.Create<string>(),
                ProjectId = _fixture.Create<Guid>(),
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(() => null)
                .Verifiable();          

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Boris Artamonov", Description = "addendum not found")]
        public async Task Invoice_End_Date_More_Than_Addendum_End_Date()
        {
            var year = DateTime.Now.Year;
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            project.Addenda = new List<SubContractors.Domain.Agreement.Addendum>();
            project.SubContractors = new List<SubContractors.Domain.SubContractor.SubContractor>();
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            addendum.StartDate = new DateTime(year, 1, 1);
            addendum.EndDate = new DateTime(year, 8, 31);
            project.Addenda.Add(addendum);
            project.SubContractors.Add(subcontractor);

            var request = new CreateInvoice
            {
                AddendumId = _fixture.Create<int>(),
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = _fixture.Create<string>(),
                ProjectId = _fixture.Create<Guid>(),
                StartDate = new DateTime(year, 9, 1),
                EndDate = new DateTime(year, 9, 30),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Boris Artamonov", Description = "invoice file not found")]
        public async Task InvoiceFile_Not_Found()
        {
            var registeredInvoice = new SubContractors.Domain.Invoice.Invoice(_fixture.Create<int>());
            registeredInvoice.InvoiceNumber = "yUrv52sE9";
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            project.Addenda = new List<SubContractors.Domain.Agreement.Addendum>();
            project.SubContractors = new List<SubContractors.Domain.SubContractor.SubContractor>();
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            project.Addenda.Add(addendum);
            project.SubContractors.Add(subcontractor);
            var paymentTerm = new SubContractors.Domain.Budget.PaymentTerm(_fixture.Create<int>());

            var request = new CreateInvoice
            {
                AddendumId = addendum.Id,
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = "yUrv52sE9",
                ProjectId = _fixture.Create<Guid>(),
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _projectSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.ProjectId, new string[]
                                                            {
                                                                nameof(SubContractors.Domain.Project.Project.SubContractors),
                                                                nameof(SubContractors.Domain.Project.Project.Addenda)
                                                            }))
                .ReturnsAsync(project)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(addendum)
                .Verifiable();

            _supportingDocumentSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.InvoiceFileId, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "invoice referral not found")]
        public async Task InvoiceReferral_Not_Found()
        {
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            subcontractor.SubContractorType = SubContractorType.HrPartner;
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            var paymentTerm = new SubContractors.Domain.Budget.PaymentTerm(_fixture.Create<int>());
            var supportingDocuments = new SupportingDocument(_fixture.Create<Guid>());
            var referral = new SubContractors.Domain.SubContractor.Staff.Staff(_fixture.Create<int>());

            var request = new CreateInvoice
            {
                AddendumId = _fixture.Create<int>(),
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = _fixture.Create<string>(),
                PaymentNumber = 1,
                ProjectId = _fixture.Create<Guid>(),
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                ReferralId = _fixture.Create<int>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(addendum)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.ReferralId,
                                                        new string[] { nameof(SubContractors.Domain.SubContractor.Staff.Staff.Invoices) }))
                .ReturnsAsync(() => null)
                .Verifiable();            

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "invoice referral with paymentNumber is equal to 1 not found")]
        public async Task ReferralWith_PaymentNumber_1_Not_Found()
        {
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            subcontractor.SubContractorType = SubContractorType.HrPartner;
            var project = new SubContractors.Domain.Project.Project(_fixture.Create<Guid>());
            var addendum = new SubContractors.Domain.Agreement.Addendum(_fixture.Create<int>());
            var paymentTerm = new SubContractors.Domain.Budget.PaymentTerm(_fixture.Create<int>());
            var supportingDocuments = new SupportingDocument(_fixture.Create<Guid>());
            var referral = new SubContractors.Domain.SubContractor.Staff.Staff(_fixture.Create<int>());
            referral.Invoices = new List<SubContractors.Domain.Invoice.Invoice>();

            var request = new CreateInvoice
            {
                AddendumId = _fixture.Create<int>(),
                Amount = _fixture.Create<decimal>(),
                Comment = _fixture.Create<string>(),
                EndDate = _fixture.Create<DateTime>(),
                InvoiceDate = _fixture.Create<DateTime>(),
                InvoiceFileId = _fixture.Create<Guid>(),
                InvoiceNumber = _fixture.Create<string>(),
                PaymentNumber = 2,
                ProjectId = _fixture.Create<Guid>(),
                StartDate = _fixture.Create<DateTime>(),
                SubContractorId = subcontractor.Id,
                TaxRate = _fixture.Create<decimal>(),
                ReferralId = _fixture.Create<int>(),
                IsUseInvoiceDateForBudgetSystem = _fixture.Create<bool>(),
            };

            _invoiceSqlRepositoryMock.Setup(x => x.GetAsync(x => x.InvoiceNumber == request.InvoiceNumber &&
                                                            x.InvoiceStatus == InvoiceStatus.SentToPay, null))
                .ReturnsAsync(() => null)
                .Verifiable();

            _subContractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, null))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _addendumSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.AddendumId, null))
                .ReturnsAsync(addendum)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.ReferralId,
                                                        new string[] { nameof(SubContractors.Domain.SubContractor.Staff.Staff.Invoices) }))
                .ReturnsAsync(() => referral)
                .Verifiable();            

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Boris Artamonov", Description = "Validation failure")]
        public async Task Create_Invoice_Validation_Failed()
        {
            var request = new CreateInvoice();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
