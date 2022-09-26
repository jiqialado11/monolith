using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.SubContractors.Commands.DeleteTax;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;

namespace SubContractor.Tests.Handlers.SubContractor.Tax
{
    [TestFixture]
    public class DeleteTaxHandlerTest : BaseTestFixture
    {
        private IRequestHandler<DeleteTax, Result<Unit>> _handler;

        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>> _taxSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private DeleteTaxValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new DeleteTaxValidator();

            _taxSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.Tax.Tax, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();

            _handler = new DeleteTaxHandler(_taxSqlRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test(Author = "Lado Jikia", Description = "Deletes tax record")]
        public async Task Delete_New_Tax()
        {
            var tax = new SubContractors.Domain.SubContractor.Tax.Tax(_fixture.Create<int>());

            var request = new DeleteTax { Id = tax.Id };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.Id, Array.Empty<string>() ))
                .ReturnsAsync(tax)
                .Verifiable();

            _taxSqlRepositoryMock.Setup(x => x.DeleteAsync(request.Id.Value)).Returns(Task.CompletedTask).Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Ok);

            _taxSqlRepositoryMock.Verify(f => f.DeleteAsync(tax.Id), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "tax not found")]
        public async Task Tax_Not_Found()
        {
            var request = new DeleteTax { Id = _fixture.Create<int>() };

            _taxSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.Id, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }
    }
}
