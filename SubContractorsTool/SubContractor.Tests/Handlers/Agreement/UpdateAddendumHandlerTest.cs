using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Agreement.Commands.UpdateAddendum;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Domain.Project;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class UpdateAddendumHandlerTest : BaseTestFixture
    {
        private IRequestHandler<UpdateAddendum, Result<Unit>> _handler;

        private Mock<IAgreementSqlRepository> _agreementSqlRepositoryMock;
        private Mock<ISqlRepository<Project, Guid>> _projectsSqlRepositoryMock;
        private Mock<ISqlRepository<PaymentTerm, int>> _paymentTermsSqlRepositoryMock;
        private Mock<ISqlRepository<Currency, int>> _currenciesSqlRepositoryMock;
        private Mock<IAddendaSqlRepository> _sqlRepository;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private UpdateAddendumValidator _validator;

        [TearDown]
        public override void TearDown()
        {

        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new UpdateAddendumValidator();

            _agreementSqlRepositoryMock =
                MockRepository.Create<IAgreementSqlRepository>();
            _projectsSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<Project, Guid>>();
            _paymentTermsSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<PaymentTerm, int>>();
            _currenciesSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<Currency, int>>();
            _sqlRepository =
                MockRepository.Create<IAddendaSqlRepository>();
            _unitOfWorkMock =
                MockRepository.Create<IUnitOfWork>();

            _handler = new UpdateAddendumHandler(
                _agreementSqlRepositoryMock.Object,
                _projectsSqlRepositoryMock.Object,
                _paymentTermsSqlRepositoryMock.Object,
                _currenciesSqlRepositoryMock.Object, 
                _sqlRepository.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test(Author = "Lado Jikia", Description = "Updates addendum")]
        public async Task Updates_Addendum()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var project = new Project(_fixture.Create<Guid>());
            var paymentTerm = new PaymentTerm(_fixture.Create<int>());
            var currency = new Currency(_fixture.Create<int>());

            var request = new UpdateAddendum
            {
                Id = addendum.Id,
                AgreementId = agreement.Id,
                ProjectIds = new List<Guid>{ project.Id },
                PaymentTermId = paymentTerm.Id,
                CurrencyId = currency.Id,
                EndDate = _fixture.Create<DateTime>(),
                StartDate = _fixture.Create<DateTime>(),
                Comment = _fixture.Create<string>(),
                IsForNonBillableProjects = _fixture.Create<bool>(),
                PaymentTermInDays = _fixture.Create<int>(),
                Title = _fixture.Create<string>(),
                Url = _fixture.Create<string>()
            };

            _sqlRepository
                .Setup(x => x.GetAsync(s => s.Id == request.Id, new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                }))
                .ReturnsAsync(addendum)
                .Verifiable();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(x => x.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(agreement)
                .Verifiable();

            _projectsSqlRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), Array.Empty<string>() ))
                .ReturnsAsync(project)
                .Verifiable();

            _paymentTermsSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.PaymentTermId, Array.Empty<string>() ))
                .ReturnsAsync(paymentTerm)
                .Verifiable();

            _currenciesSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.CurrencyId, Array.Empty<string>() ))
                .ReturnsAsync(currency)
                .Verifiable();

            _sqlRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Addendum>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Accepted);



            Expression<Func<Addendum, bool>> match = f =>
                f.Id == request.Id &&
                f.Title == request.Title;

            _sqlRepository.Verify(f => f.UpdateAsync(It.Is(match)), Times.Once);

        }

        [Test(Author = "Lado Jikia", Description = "Addendum not found")]
        public async Task Addendum_Not_Found()
        {
            var request = new UpdateAddendum
            {
                Id = _fixture.Create<int>()
            };

            _sqlRepository
                .Setup(x => x.GetAsync(s => s.Id == request.Id, new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                }))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Agreement not found")]
        public async Task Agreement_Not_Found()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var request = _fixture.Create<UpdateAddendum>();

            _sqlRepository
                .Setup(x => x.GetAsync(s => s.Id == request.Id, new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                }))
                .ReturnsAsync(addendum)
                .Verifiable();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(x => x.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

     
        [Test(Author = "Lado Jikia", Description = "Payment term not found")]
        public async Task Payment_Term_Not_Found()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var project = new Project(_fixture.Create<Guid>());

            var request = _fixture.Create<UpdateAddendum>();

            _sqlRepository
                .Setup(x => x.GetAsync(s => s.Id == request.Id, new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                }))
                .ReturnsAsync(addendum)
                .Verifiable();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(x => x.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(agreement)
                .Verifiable();

            _projectsSqlRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), Array.Empty<string>() ))
                .ReturnsAsync(project)
                .Verifiable();

            _paymentTermsSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.PaymentTermId, Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Currency not found")]
        public async Task Currency_Not_Found()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var project = new Project(_fixture.Create<Guid>());
            var paymentTerm = new PaymentTerm(_fixture.Create<int>());

            var request = _fixture.Create<UpdateAddendum>();

            _sqlRepository
                .Setup(x => x.GetAsync(s => s.Id == request.Id, new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                }))
                .ReturnsAsync(addendum)
                .Verifiable();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(x => x.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(agreement)
                .Verifiable();

            _projectsSqlRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), Array.Empty<string>() ))
                .ReturnsAsync(project)
                .Verifiable();

            _paymentTermsSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.PaymentTermId, Array.Empty<string>() ))
                .ReturnsAsync(paymentTerm)
                .Verifiable();

            _currenciesSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.CurrencyId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Project not found")]
        public async Task Project_Not_Found()
        {
             var addendum = new Addendum(_fixture.Create<int>());
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var paymentTerm = new PaymentTerm(_fixture.Create<int>());
            var currency = new Currency(_fixture.Create<int>());

            var request = _fixture.Create<UpdateAddendum>();

            _sqlRepository
                .Setup(x => x.GetAsync(s => s.Id == request.Id, new string[]
                {
                    nameof(Addendum.PaymentTerm),
                    nameof(Addendum.Projects),
                    nameof(Addendum.Currency),
                    nameof(Addendum.Agreement)
                }))
                .ReturnsAsync(addendum)
                .Verifiable();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(x => x.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(agreement)
                .Verifiable();

            _projectsSqlRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();

            _paymentTermsSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.PaymentTermId, Array.Empty<string>() ))
                .ReturnsAsync(paymentTerm)
                .Verifiable();

            _currenciesSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.CurrencyId, Array.Empty<string>() ))
                .ReturnsAsync(currency)
                .Verifiable();


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Update_Addendum_Validation_Failed()
        {
            var request = new UpdateAddendum();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
