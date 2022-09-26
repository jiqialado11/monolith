using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxTypesQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractor.Tests.Handlers.SubContractor.Tax
{
    [TestFixture]
    public class GetTaxTypesQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetTaxTypesQuery, Result<IList<GetTaxTypeDto>>> _handler;

        private Mock<ISqlRepository<TaxType, int>> _taxTypeSqlRepositoryMock;
        private Fixture _fixture;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();

            _taxTypeSqlRepositoryMock = MockRepository.Create<ISqlRepository<TaxType, int>>();
            _handler = new GetTaxTypesQueryHandler(_taxTypeSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of tax types")]
        public async Task Returns_Tax_Types()
        {
            var taxTypes = _fixture.CreateMany<TaxType>(10);

            _taxTypeSqlRepositoryMock.Setup(x => x.FindAsync(s => true, Array.Empty<string>() )).ReturnsAsync(taxTypes);

            var request = new GetTaxTypesQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(taxTypes.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found status in case there are no tax types")]
        public async Task Returns_Not_Found_For_BudgetOffices()
        {
            var taxTypes = _fixture.CreateMany<TaxType>(0);

            _taxTypeSqlRepositoryMock.Setup(x => x.FindAsync(s => true, Array.Empty<string>() )).ReturnsAsync(taxTypes);

            var request = new GetTaxTypesQuery();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }
    }
}
