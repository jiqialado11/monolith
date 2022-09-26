using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Invoices.Queries.GetAllInvoicesPagedQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractor.Tests.Handlers.Invoice
{
    [TestFixture]
    public class GetAllInvoicesQueryHandlerTest : BaseTestFixture
    {
        private Mock<IInvoiceSqlRepository> _invoiceSqlRepositoryMock;

        private IRequestHandler<GetAllInvoicesPagedQuery, Result<PagedResult<GetAllInvoicesPagedDto>>> _handler;

        private Fixture _fixture;

         [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _fixture.Customizations.Add(new InvoiceSpecimenBuilder(_fixture.Create<int>()));

            _invoiceSqlRepositoryMock =
                MockRepository.Create<IInvoiceSqlRepository>();

            _invoiceSqlRepositoryMock = MockRepository.Create<IInvoiceSqlRepository>();
           
            _handler = new GetAllInvoicesPagedQueryHandler(_invoiceSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "Get all invoices from database")]
        public async Task Return_All_Invoices()
        {
            var invoicesPerPage = 10;
            var invoices = _fixture.CreateMany<SubContractors.Domain.Invoice.Invoice>(invoicesPerPage);

            var invocePagedResult = PagedResult<SubContractors.Domain.Invoice.Invoice>
                                              .Create(invoices,
                                                      1,
                                                      invoicesPerPage,
                                                      1,
                                                      invoicesPerPage);

            var request = new GetAllInvoicesPagedQuery();

            _invoiceSqlRepositoryMock.Setup(
                            x => x.BrowseAsync(
                                    x => x.IsDeleted == false,
                                    request))
                                     .ReturnsAsync(invocePagedResult);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
        }

        [Test(Author = "Mykolay Levkovskyi", Description = "Invoices list")]
        public async Task Invoices_Not_Found()
        {
            var invoicesPerPage = 10;
            var invoices = new List<SubContractors.Domain.Invoice.Invoice>();

            var invocePagedResult = PagedResult<SubContractors.Domain.Invoice.Invoice>
                                              .Create(invoices,
                                                      1,
                                                      invoicesPerPage,
                                                      1,
                                                      invoicesPerPage);
            var request = new GetAllInvoicesPagedQuery();

            _invoiceSqlRepositoryMock.Setup(
                            x => x.BrowseAsync(
                                    x => x.IsDeleted == false,
                                    request))
                                     .ReturnsAsync(invocePagedResult);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }
    }
}
