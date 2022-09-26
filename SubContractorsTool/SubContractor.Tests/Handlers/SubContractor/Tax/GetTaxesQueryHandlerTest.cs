using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.SubContractors.Queries.GetTaxesQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractor.Tests.Handlers.SubContractor.Tax
{
    [TestFixture]
    public class GetTaxesQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetTaxesQuery, Result<IList<GetTaxesDto>>> _handler;

        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>> _taxSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>> _subContractorSqlRepositoryMock;
        private Fixture _fixture;
        private GetTaxesQueryValidator _validator;

        private int _subContractorId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new GetTaxesQueryValidator();

            _subContractorId = _fixture.Create<int>();

            _fixture.Customizations.Add(new TaxSpecimenBuilder(_subContractorId));

            _taxSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>>();
            _subContractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _handler = new GetTaxesQueryHandler(_subContractorSqlRepositoryMock.Object, _taxSqlRepositoryMock.Object,
                Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of subcontractor taxes")]
        public async Task Returns_Taxes()
        {
            var taxes = _fixture.CreateMany<SubContractors.Domain.SubContractor.Tax.Tax>(10);
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());

            var request = new GetTaxesQuery { SubContractorId = subContractor.Id };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value, Array.Empty<string>() ))
                .ReturnsAsync(subContractor);

            _taxSqlRepositoryMock.Setup(x => x.FindAsync(c => c.SubContractor.Id == request.SubContractorId.Value,
                    new string[] { nameof(SubContractors.Domain.SubContractor.Tax.Tax.SubContractor), nameof(SubContractors.Domain.SubContractor.Tax.Tax.TaxType) }))
                .ReturnsAsync(taxes);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(taxes.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "SubContractor not found")]
        public async Task SubContractor_Not_Found()
        {
            var request = new GetTaxesQuery { SubContractorId = _fixture.Create<int>() };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value, Array.Empty<string>() ))
                .ReturnsAsync(() => null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "taxes not found for subcontractor")]
        public async Task Taxes_Not_Found()
        {
            var taxes = _fixture.CreateMany<SubContractors.Domain.SubContractor.Tax.Tax>(0);
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());

            var request = new GetTaxesQuery { SubContractorId = subContractor.Id };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value, Array.Empty<string>() ))
                .ReturnsAsync(subContractor);

            _taxSqlRepositoryMock.Setup(x => x.FindAsync(c => c.SubContractor.Id == request.SubContractorId.Value,
                    new string[] { nameof(SubContractors.Domain.SubContractor.Tax.Tax.SubContractor), nameof(SubContractors.Domain.SubContractor.Tax.Tax.TaxType) }))
                .ReturnsAsync(taxes);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.IsNull(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Taxes_Validation_Failed()
        {
            var request = new GetTaxesQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }

    public class TaxSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;
        private readonly int _subContractorId;

        public TaxSpecimenBuilder(int subContractorId)
        {
            this._subContractorId = subContractorId;
            _fixture = new Fixture();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(SubContractors.Domain.SubContractor.Tax.Tax))
            {
                return new SubContractors.Domain.SubContractor.Tax.Tax
                {
                    SubContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId),
                    Date = _fixture.Create<DateTime>(),
                    TaxType = _fixture.Create<TaxType>(),
                    Link = _fixture.Create<string>(),
                    Name = _fixture.Create<string>(),
                    TaxNumber = _fixture.Create<string>()
                };
            }

            return new NoSpecimen();
        }
    }
}
