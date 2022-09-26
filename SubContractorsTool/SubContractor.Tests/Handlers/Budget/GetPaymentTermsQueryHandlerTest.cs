using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Budget.Queries.GetPaymentTermsQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Budget;

namespace SubContractor.Tests.Handlers.Budget
{
    [TestFixture]
    public class GetPaymentTermsQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetPaymentTermsQuery, Result<IList<GetPaymentTermsDto>>> _handler;

        private Mock<ISqlRepository<PaymentTerm, int>> _paymentTermsSqlRepositoryMock;
        private Fixture _fixture;


        [SetUp]
        public override void SetUp()
        {

            base.SetUp();

            _fixture = new Fixture();

            _paymentTermsSqlRepositoryMock = MockRepository.Create<ISqlRepository<PaymentTerm, int>>();
            _handler = new GetPaymentTermsQueryHandler(_paymentTermsSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of active payment terms")]
        public async Task Returns_Active_Payment_Terms()
        {

            var paymentTerms = _fixture.CreateMany<PaymentTerm>(10);


            _paymentTermsSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => true, Array.Empty<string>() ))
                .ReturnsAsync(paymentTerms);

            var request = new GetPaymentTermsQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(paymentTerms.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not Found status in case there are no active payment terms")]
        public async Task Returns_Not_Found_For_Payment_Terms()
        {

            var paymentTerms = _fixture.CreateMany<PaymentTerm>(0);

            _paymentTermsSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => true, Array.Empty<string>() ))
                .ReturnsAsync(paymentTerms);

            var request = new GetPaymentTermsQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }
    }
}
