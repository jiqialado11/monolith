using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.SubContractors.Commands.CreateTax;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor.Tax;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractor.Tests.Handlers.SubContractor.Tax
{
    [TestFixture]
    public class CreateTaxHandlerTest : BaseTestFixture
    {
        private IRequestHandler<CreateTax, Result<int>> _handler;

        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>> _taxSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>> _subcontractorSqlRepositoryMock;
        private Mock<ISqlRepository<TaxType, int>> _taxTypeSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private CreateTaxValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new CreateTaxValidator();

            _taxSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>>();
            _subcontractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();
            _taxTypeSqlRepositoryMock = MockRepository.Create<ISqlRepository<TaxType, int>>();

            _handler = new CreateTaxHandler(_subcontractorSqlRepositoryMock.Object, _taxSqlRepositoryMock.Object,
                _unitOfWorkMock.Object, _taxTypeSqlRepositoryMock.Object);
        }

        [Test(Author = "Lado Jikia", Description = "Creates new tax record")]
        public async Task Create_New_Tax()
        {
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var taxType = new TaxType(_fixture.Create<int>());

            var request = new CreateTax
            {
                Date = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                TaxTypeId = taxType.Id,
                SubContractorId = subcontractor.Id,
                TaxNumber = _fixture.Create<string>(),
                Url = _fixture.Create<string>()
            };

            _subcontractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, Array.Empty<string>() ))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _taxTypeSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.TaxTypeId, Array.Empty<string>() ))
                .ReturnsAsync(taxType)
                .Verifiable();

            _taxSqlRepositoryMock
                .Setup(x => x.AddAsync(It.Is<SubContractors.Domain.SubContractor.Tax.Tax>(
                    c => c.SubContractor.Id == request.SubContractorId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);

            Expression<Func<SubContractors.Domain.SubContractor.Tax.Tax, bool>> match = f =>
                f.SubContractor.Id == request.SubContractorId && f.Link == request.Url && f.Date == request.Date &&
                f.Name == request.Name && f.TaxType.Id == request.TaxTypeId && f.TaxNumber == request.TaxNumber;

            _taxSqlRepositoryMock.Verify(f => f.AddAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "subcontractor not found")]
        public async Task SubContractor_Not_Found()
        {
            var request = new CreateTax
            {
                Date = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                TaxTypeId = _fixture.Create<int>(),
                SubContractorId = _fixture.Create<int>(),
                TaxNumber = _fixture.Create<string>(),
                Url = _fixture.Create<string>()
            };

            _subcontractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "tax type not found")]
        public async Task Tax_Type_Not_Found()
        {
            var subcontractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());

            var request = new CreateTax
            {
                Date = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                TaxTypeId = _fixture.Create<int>(),
                SubContractorId = subcontractor.Id,
                TaxNumber = _fixture.Create<string>(),
                Url = _fixture.Create<string>()
            };

            _subcontractorSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.SubContractorId, Array.Empty<string>() ))
                .ReturnsAsync(subcontractor)
                .Verifiable();

            _taxTypeSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.TaxTypeId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Create_Tax_Validation_Failed()
        {
            var request = new CreateTax();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
