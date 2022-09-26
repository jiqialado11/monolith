using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Agreement.Commands.AddRate;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.SubContractor.Staff;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class AddRateHandlerTest : BaseTestFixture
    {
        private IRequestHandler<AddRate, Result<int>> _handler;

        private Mock<IAddendaSqlRepository> _sqlRepository;
        private Mock<ISqlRepository<RateUnit, int>> _rateUnitsSqlRepositoryMock;
        private Mock<ISqlRepository<Staff, int>> _staffsSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ISqlRepository<Rate, int>> _rateSqlRepository;
        private Fixture _fixture;

        private AddRateValidator _validator;

        [TearDown]
        public override void TearDown()
        {

        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new AddRateValidator();

            _sqlRepository =
                MockRepository.Create<IAddendaSqlRepository>();
            _rateUnitsSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<RateUnit, int>>();
            _staffsSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<Staff, int>>();
            _unitOfWorkMock =
                MockRepository.Create<IUnitOfWork>();
            _rateSqlRepository = MockRepository.Create<ISqlRepository<Rate, int>>();

            _handler = new AddRateHandler(
                _sqlRepository.Object,
                _rateUnitsSqlRepositoryMock.Object,
                _staffsSqlRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _rateSqlRepository.Object
            );
        }

        [Test(Author = "Lado Jikia", Description = "Add rate to addendum")]
        public async Task Add_Rate()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var rateUnit = new RateUnit(_fixture.Create<int>());
            var staff = new Staff(_fixture.Create<int>());

            var request = new AddRate
            {
                Rate = _fixture.Create<decimal>(),
                Description = _fixture.Create<string>(),
                FromDate = _fixture.Create<DateTime>(),
                ToDate = _fixture.Create<DateTime>(),
                AddendumId = addendum.Id,
                Name = _fixture.Create<string>(),
                RateUnitId = rateUnit.Id,
                StaffId = staff.Id
            };

            _sqlRepository
                .Setup(x => x.GetAsync(request.AddendumId.Value, Array.Empty<string>() ))
                .ReturnsAsync(addendum)
                .Verifiable();

            _staffsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.StaffId.Value, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _rateUnitsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.RateUnitId.Value, Array.Empty<string>() ))
                .ReturnsAsync(rateUnit)
                .Verifiable();

            _rateSqlRepository
                .Setup(x => x.AddAsync(It.IsAny<Rate>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);
            Assert.IsNotNull(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Addendum not found")]
        public async Task Addendum_Not_Found()
        {
            var request = _fixture.Create<AddRate>();

            _sqlRepository
                .Setup(x => x.GetAsync(request.AddendumId.Value, Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Staff not found")]
        public async Task Staff_Not_Found()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var request = new AddRate
            {
                AddendumId = addendum.Id,
                StaffId = _fixture.Create<int>()
            };

            _sqlRepository
                .Setup(x => x.GetAsync(request.AddendumId.Value, Array.Empty<string>() ))
                .ReturnsAsync(addendum)
                .Verifiable();

            _staffsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.StaffId.Value, Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Rate unit not found")]
        public async Task Rate_Unit_Not_Found()
        {
            var addendum = new Addendum(_fixture.Create<int>());
            var staff = new Staff(_fixture.Create<int>());
            var request = new AddRate
            {
                AddendumId = addendum.Id,
                StaffId = staff.Id,
                RateUnitId = _fixture.Create<int>()
            };

            _sqlRepository
                .Setup(x => x.GetAsync(request.AddendumId.Value, Array.Empty<string>() ))
                .ReturnsAsync(addendum)
                .Verifiable();

            _staffsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.StaffId.Value, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _rateUnitsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.RateUnitId.Value, Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Add_rate_Validation_Failed()
        {
            var request = new AddRate();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
