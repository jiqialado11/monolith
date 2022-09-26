using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.SubContractors.Commands.UpdateTax;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractor.Tests.Handlers.SubContractor.Tax
{
    [TestFixture]
    public class UpdateTaxHandlerTest : BaseTestFixture
    {
        private IRequestHandler<UpdateTax, Result<Unit>> _handler;

        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>> _taxSqlRepositoryMock;
        private Mock<ISqlRepository<TaxType, int>> _taxTypeSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private UpdateTaxValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new UpdateTaxValidator();

            _taxSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();
            _taxTypeSqlRepositoryMock = MockRepository.Create<ISqlRepository<TaxType, int>>();

            _handler = new UpdateTaxHandler(_taxSqlRepositoryMock.Object, _unitOfWorkMock.Object,
                _taxTypeSqlRepositoryMock.Object);
        }

        [Test(Author = "Lado Jikia", Description = "updates existing tax record")]
        public async Task Update_Tax_OK()
        {
            var taxType = new TaxType(_fixture.Create<int>());
            var tax = new SubContractors.Domain.SubContractor.Tax.Tax(_fixture.Create<int>());

            var request = new UpdateTax
            {
                Date = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                TaxTypeId = taxType.Id,
                TaxNumber = _fixture.Create<string>(),
                Url = _fixture.Create<string>(),
                Id = tax.Id
            };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.Id, Array.Empty<string>() ))
                .ReturnsAsync(tax)
                .Verifiable();

            _taxTypeSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.TaxTypeId, Array.Empty<string>() ))
                .ReturnsAsync(taxType)
                .Verifiable();

            _taxSqlRepositoryMock
                .Setup(x => x.UpdateAsync(It.Is<SubContractors.Domain.SubContractor.Tax.Tax>(c => c.Id == request.Id)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Ok);

            Expression<Func<SubContractors.Domain.SubContractor.Tax.Tax, bool>> match = f =>
                f.Link == request.Url && f.Date == request.Date && f.Name == request.Name &&
                f.TaxType.Id == request.TaxTypeId && f.Id == request.Id && f.TaxNumber == request.TaxNumber;

            _taxSqlRepositoryMock.Verify(f => f.UpdateAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "tax not found")]
        public async Task Tax_Not_Found()
        {
            var request = new UpdateTax
            {
                Date = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                TaxTypeId = _fixture.Create<int>(),
                TaxNumber = _fixture.Create<string>(),
                Url = _fixture.Create<string>(),
                Id = _fixture.Create<int>()
            };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.Id, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "tax type not found")]
        public async Task Tax_Type_Not_Found()
        {
            var tax = new SubContractors.Domain.SubContractor.Tax.Tax(_fixture.Create<int>());

            var request = new UpdateTax
            {
                Date = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                TaxTypeId = _fixture.Create<int>(),
                TaxNumber = _fixture.Create<string>(),
                Url = _fixture.Create<string>(),
                Id = tax.Id
            };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.Id, Array.Empty<string>() ))
                .ReturnsAsync(tax)
                .Verifiable();

            _taxTypeSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.TaxTypeId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Update_Tax_Validation_Failed()
        {
            var request = new UpdateTax();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
