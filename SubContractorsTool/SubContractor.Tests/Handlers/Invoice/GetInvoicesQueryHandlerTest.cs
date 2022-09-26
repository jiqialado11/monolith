using AutoFixture;
using AutoFixture.Kernel;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Invoices.Queries.GetInvoicesQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Invoice;
using SubContractors.Domain.Project;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InvoiceModel = SubContractors.Domain.Invoice.Invoice;
using SubContractorModel = SubContractors.Domain.SubContractor.SubContractor;

namespace SubContractor.Tests.Handlers.Invoice
{
    [TestFixture]
    public class GetInvoicesQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetInvoicesQuery, Result<IList<GetInvoicesDto>>> _handler;

        private Mock<IInvoiceSqlRepository> _invoiceSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractorModel, int>> _subContractorSqlRepositoryMock;

        private Fixture _fixture;

        private int _subContractorId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _subContractorId = _fixture.Create<int>();
            _fixture.Customizations.Add(new InvoiceSpecimenBuilder(_subContractorId));

            _invoiceSqlRepositoryMock =
                MockRepository.Create<IInvoiceSqlRepository>();
            _subContractorSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<SubContractorModel, int>>();

            _invoiceSqlRepositoryMock = MockRepository.Create<IInvoiceSqlRepository>();
            _subContractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _handler = new GetInvoicesQueryHandler(_invoiceSqlRepositoryMock.Object, _subContractorSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "Get all invoices of subcontractor")]
        public async Task Return_Invoices()
        {
            var invoices = _fixture.CreateMany<InvoiceModel>(10);
            var subContractor = new SubContractorModel();

            var request = new GetInvoicesQuery { SubContractorId = subContractor.Id };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.SubContractorId.Value, null))
                                           .ReturnsAsync(subContractor);

            _invoiceSqlRepositoryMock.Setup(x => x.FindAsync(c => c.SubContractor == subContractor))
                                     .ReturnsAsync(invoices);

            var result = await _handler.Handle(request, CancellationToken.None);
           
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(invoices.Count(), result.Data.Count);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "SubContractor not found")]
        public async Task SubContractor_Not_Found()
        {
            var invoices = _fixture.CreateMany<InvoiceModel>(10);
            var subContractor = new SubContractorModel();

            var request = new GetInvoicesQuery { SubContractorId = subContractor.Id };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.SubContractorId.Value, null))
                                           .ReturnsAsync(() => null);


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "Invoices list of subcontractor not found")]
        public async Task Invoices_Not_Found()
        {
            var invoices = _fixture.CreateMany<InvoiceModel>(10);
            var subContractor = new SubContractorModel();

            var request = new GetInvoicesQuery { SubContractorId = subContractor.Id };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(x => x.Id == request.SubContractorId.Value, null))
                                           .ReturnsAsync(subContractor);

            _invoiceSqlRepositoryMock.Setup(x => x.FindAsync(c => c.SubContractor == subContractor))
                                     .ReturnsAsync(() => new List<InvoiceModel>());

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }
    }

    public class InvoiceSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;
        private readonly int _subContractorId;

        public InvoiceSpecimenBuilder(int subContractorId)
        {
            this._subContractorId = subContractorId;
            _fixture = new Fixture();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(SubContractors.Domain.Invoice.Invoice))
            {
                return new SubContractors.Domain.Invoice.Invoice
                {
                    SubContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId),
                    InvoiceDate = _fixture.Create<DateTime>(),
                    EndDate = _fixture.Create<DateTime>(),
                    StartDate = _fixture.Create<DateTime>(),
                    Addendum = new Addendum(_fixture.Create<int>()),
                    Amount = _fixture.Create<decimal>(),
                    Comment = _fixture.Create<string>(),
                    SupportingDocuments = new List<SupportingDocument> { new(_fixture.Create<Guid>()) },
                    InvoiceFile = new SupportingDocument(),
                    InvoiceNumber = _fixture.Create<string>(),
                    InvoiceStatus = _fixture.Create<InvoiceStatus>(),
                    MileStone = new Milestone(),
                    PaymentNumber = _fixture.Create<int>(),
                    IsDeleted = _fixture.Create<bool>(),
                    TaxAmount = _fixture.Create<decimal>(),
                    TaxRate = _fixture.Create<decimal>(),
                    Project = new Project(),
                };
            }

            return new NoSpecimen();
        }
    }
}
