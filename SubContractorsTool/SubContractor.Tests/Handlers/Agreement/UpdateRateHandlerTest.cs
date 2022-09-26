using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Agreement.Commands.UpdateRate;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class UpdateRateHandlerTest : BaseTestFixture
    {
        private IRequestHandler<UpdateRate, Result<Unit>> _handler;
        
        private Mock<ISqlRepository<RateUnit, int>> _rateUnitsSqlRepositoryMock;
        private Mock<ISqlRepository<Rate, int>> _rateSqlRepositoryMock;
        private Mock<ISqlRepository<Staff, int>> _staffsSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private UpdateRateValidator _validator;

        [TearDown]
        public override void TearDown()
        {

        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new UpdateRateValidator();

            _rateSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<Rate, int>>();
            _rateUnitsSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<RateUnit, int>>();
            _staffsSqlRepositoryMock =
                MockRepository.Create<ISqlRepository<Staff, int>>();
            _unitOfWorkMock =
                MockRepository.Create<IUnitOfWork>();

            _handler = new UpdateRateHandler(
                _rateUnitsSqlRepositoryMock.Object,
                _staffsSqlRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _rateSqlRepositoryMock.Object
            );
        }

        [Test(Author = "Lado Jikia", Description = "Update rate")]
        public async Task Update_Rate()
        {
            var rate = new Rate(_fixture.Create<int>());
            var rateUnit = new RateUnit(_fixture.Create<int>());
            var staff = new Staff(_fixture.Create<int>());

            var request = new UpdateRate
            {
                Rate = _fixture.Create<decimal>(),
                Description = _fixture.Create<string>(),
                FromDate = _fixture.Create<DateTime>(),
                ToDate = _fixture.Create<DateTime>(),
                Name = _fixture.Create<string>(),
                RateUnitId = rateUnit.Id,
                StaffId = staff.Id,
                Id = rate.Id
            };

            _rateSqlRepositoryMock
                .Setup(x => x.GetAsync(request.Id.Value, new string[] { nameof(Rate.Staff), nameof(Rate.Unit) }))
                .ReturnsAsync(rate)
                .Verifiable();

            _staffsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.StaffId.Value, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _rateUnitsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.RateUnitId.Value, Array.Empty<string>() ))
                .ReturnsAsync(rateUnit)
                .Verifiable();

            _rateSqlRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Rate>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Accepted);
            Assert.IsNotNull(result.Data);

            Expression<Func<Rate, bool>> match = f =>
                f.Description == request.Description &&
                f.Name == request.Name &&
                f.Id == request.Id &&
                f.RateValue == request.Rate &&
                f.FromDate == request.FromDate;
            
            _rateSqlRepositoryMock.Verify(f => f.UpdateAsync(It.Is(match)), Times.Once);

        }

        [Test(Author = "Lado Jikia", Description = "Rate not found")]
        public async Task Rate_Not_Found()
        {
            var request = _fixture.Create<UpdateRate>();

            _rateSqlRepositoryMock
                .Setup(x => x.GetAsync(request.Id.Value, new string[] { nameof(Rate.Staff), nameof(Rate.Unit) }))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Staff not found")]
        public async Task Staff_Not_Found()
        {
            var rate = new Rate(_fixture.Create<int>());

            var request = new UpdateRate
            {
                Id = rate.Id,
                StaffId = _fixture.Create<int>()
            };

            _rateSqlRepositoryMock
                .Setup(x => x.GetAsync(request.Id.Value, new string[] { nameof(Rate.Staff), nameof(Rate.Unit) }))
                .ReturnsAsync(rate)
                .Verifiable();

            _staffsSqlRepositoryMock
                .Setup(x => x.GetAsync(request.StaffId.Value, Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Rate Unit not found")]
        public async Task Rate_Unit_Not_Found()
        {
            var rate = new Rate(_fixture.Create<int>());
            var staff = new Staff(_fixture.Create<int>());

            var request = new UpdateRate
            {
                Id = rate.Id,
                StaffId = staff.Id,
                RateUnitId = _fixture.Create<int>()
            };

            _rateSqlRepositoryMock
                .Setup(x => x.GetAsync(request.Id.Value, new string[] { nameof(Rate.Staff), nameof(Rate.Unit) }))
                .ReturnsAsync(rate)
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
        public async Task Update_rate_Validation_Failed()
        {
            var request = new UpdateRate();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
