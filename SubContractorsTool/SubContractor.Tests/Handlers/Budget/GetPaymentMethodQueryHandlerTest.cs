using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Budget.Queries.GetPaymentMethodsQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Budget;

namespace SubContractor.Tests.Handlers.Budget
{
    [TestFixture]
    public class GetPaymentMethodQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetPaymentMethodQuery, Result<IList<GetPaymentMethodsDto>>> _handler;

        private Mock<ISqlRepository<PaymentMethod, int>> _paymentMethodSqlRepositoryMock;
        private Fixture _fixture;

        [SetUp]
        public override void SetUp()
        {

            base.SetUp();

            _fixture = new Fixture();

            _paymentMethodSqlRepositoryMock = MockRepository.Create<ISqlRepository<PaymentMethod, int>>();
            _handler = new GetPaymentMethodQueryHandler(_paymentMethodSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of active payment methods")]
        public async Task Returns_Active_Payment_Methods()
        {

            var paymentMethods = _fixture.CreateMany<PaymentMethod>(10);


            _paymentMethodSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => true, Array.Empty<string>() ))
                .ReturnsAsync(paymentMethods);

            var request = new GetPaymentMethodQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(paymentMethods.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not Found status in case there are no active payment methods")]
        public async Task Returns_Not_Found_For_Payment_Method()
        {

            var paymentMethod = _fixture.CreateMany<PaymentMethod>(0);

            _paymentMethodSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => true, new string [] { }))
                .ReturnsAsync(paymentMethod);

            var request = new GetPaymentMethodQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

    }
}
