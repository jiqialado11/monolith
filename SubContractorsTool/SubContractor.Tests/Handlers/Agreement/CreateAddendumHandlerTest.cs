using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Agreement.Commands.CreateAddendum;
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
    public class CreateAddendumHandlerTest : BaseTestFixture
    {
        private IRequestHandler<CreateAddendum, Result<int>> _handler;
        
        private Mock<IAgreementSqlRepository> _agreementSqlRepositoryMock;
        private Mock<ISqlRepository<Project, Guid>> _projectsSqlRepositoryMock;
        private Mock<ISqlRepository<PaymentTerm, int>> _paymentTermsSqlRepositoryMock;
        private Mock<ISqlRepository<Currency, int>> _currenciesSqlRepositoryMock;
        private Mock<IAddendaSqlRepository> _sqlRepository;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private CreateAddendumValidator _validator;

        [TearDown]
        public override void TearDown()
        {
            
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new CreateAddendumValidator();
            
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

            _handler = new CreateAddendumHandler(
                _projectsSqlRepositoryMock.Object,
                _paymentTermsSqlRepositoryMock.Object,
                _currenciesSqlRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _agreementSqlRepositoryMock.Object,
                _sqlRepository.Object
            );
        }

        [Test(Author = "Lado Jikia", Description = "Creates addendum")]
        public async Task Create_Addendum()
        {
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var project = new Project(_fixture.Create<Guid>());
            var paymentTerm = new PaymentTerm(_fixture.Create<int>());
            var currency = new Currency(_fixture.Create<int>());    

            var request = new CreateAddendum
            {
                AgreementId = agreement.Id,
                PaymentTermId = paymentTerm.Id,
                ProjectIds = new List<Guid>{ project.Id },
                CurrencyId = currency.Id,
                EndDate = _fixture.Create<DateTime>(),
                StartDate = _fixture.Create<DateTime>(),
                Comment = _fixture.Create<string>(),
                IsForNonBillableProjects = _fixture.Create<bool>(),
                PaymentTermInDays = _fixture.Create<int>(),
                Title = _fixture.Create<string>(),
                Url = _fixture.Create<string>()
            };
            
            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.AgreementId, Array.Empty<string>() ))
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
                .Setup(x => x.AddAsync(It.IsAny<Addendum>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);
            Assert.IsNotNull(result.Data);
            

            Expression<Func<Addendum, bool>> match = f =>
                f.Agreement.Id == request.AgreementId &&
                f.PaymentTerm.Id == request.PaymentTermId &&
                f.Currency.Id == request.CurrencyId &&
                f.Title == request.Title;

            _sqlRepository.Verify(f => f.AddAsync(It.Is(match)), Times.Once);
        }


        [Test(Author = "Lado Jikia", Description = "Agreement not found")]
        public async Task Agreement_Not_Found()
        {
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var request = _fixture.Create<CreateAddendum>();
            
            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

       

        [Test(Author = "Lado Jikia", Description = "Payment Term not found")]
        public async Task Payment_Term_Not_Found()
        {
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var project = new Project(_fixture.Create<Guid>());
            var request = _fixture.Create<CreateAddendum>();
            

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(agreement)
                .Verifiable();

            _projectsSqlRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), Array.Empty<string>() ))
                .ReturnsAsync(project)
                .Verifiable();

            _paymentTermsSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.PaymentTermId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();


            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Currency not found")]
        public async Task Currency_Not_Found()
        {
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var project = new Project(_fixture.Create<Guid>());
            var paymentTerm = new PaymentTerm(_fixture.Create<int>());
            var request = _fixture.Create<CreateAddendum>();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.AgreementId, Array.Empty<string>() ))
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
            var agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>());
            var paymentTerm = new PaymentTerm(_fixture.Create<int>());
            var currency = new Currency(_fixture.Create<int>());
            var request = _fixture.Create<CreateAddendum>();

            _agreementSqlRepositoryMock
                .Setup(x => x.GetAsync(s => s.Id == request.AgreementId, Array.Empty<string>() ))
                .ReturnsAsync(agreement)
                .Verifiable();

            _projectsSqlRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), Array.Empty<string>() ))
                .ReturnsAsync(() => null)
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
        public async Task Create_Addendum_Validation_Failed()
        {
            var request = new CreateAddendum();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }


}
