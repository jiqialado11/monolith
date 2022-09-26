using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;

namespace SubContractor.Tests.Handlers.SubContractor.Tax
{
    [TestFixture]
    public class GetTaxQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetTaxQuery, Result<GetTaxDto>> _handler;

        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>> _taxSqlRepositoryMock;
        private Fixture _fixture;
        private GetTaxQueryValidator _validator;

        private int _subContractorId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new GetTaxQueryValidator();
            _subContractorId = _fixture.Create<int>();
            _fixture.Customizations.Add(new TaxSpecimenBuilder(_subContractorId));

            _taxSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>>();
            _handler = new GetTaxQueryHandler(_taxSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns tax with provided identifier")]
        public async Task Returns_Tax()
        {
            var tax = _fixture.Create<SubContractors.Domain.SubContractor.Tax.Tax>();

            var request = new GetTaxQuery { Id = tax.Id };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(request.Id.Value,
                    new[] { nameof(SubContractors.Domain.SubContractor.Tax.Tax.SubContractor),  nameof(SubContractors.Domain.SubContractor.Tax.Tax.TaxType) }))
                .ReturnsAsync(tax);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(tax.Id, result.Data.Id);
            Assert.AreEqual(tax.Date, result.Data.Date);
            Assert.AreEqual(tax.Name, result.Data.Name);
        }

        [Test(Author = "Lado Jikia", Description = "tax not found")]
        public async Task Tax_Not_Found()
        {
            var request = new GetTaxQuery { Id = _fixture.Create<int>() };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(request.Id.Value,
                    new[] { nameof(SubContractors.Domain.SubContractor.Tax.Tax.SubContractor),  nameof(SubContractors.Domain.SubContractor.Tax.Tax.TaxType) }))
                .ReturnsAsync(() => null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Tax_Validation_Failed()
        {
            var request = new GetTaxQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
