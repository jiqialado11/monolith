using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Check.Commands.UpdateBackgroundCheck;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractor.Tests.Handlers.Check
{
    [TestFixture]
    public class UpdateBackgroundCheckHandlerTest : BaseTestFixture
    {
        private IRequestHandler<UpdateBackgroundCheck, Result<Unit>> _handler;

        private Mock<ISqlRepository<BackgroundCheck, int>> _backgroundCheckSqlRepositoryMock;
        private Mock<ISqlRepository<Staff, int>> _staffSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private UpdateBackgroundCheckValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new UpdateBackgroundCheckValidator();

            _backgroundCheckSqlRepositoryMock = MockRepository.Create<ISqlRepository<BackgroundCheck, int>>();
            _staffSqlRepositoryMock = MockRepository.Create<ISqlRepository<Staff, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();

            _handler = new UpdateBackgroundCheckHandler(_backgroundCheckSqlRepositoryMock.Object,
                _unitOfWorkMock.Object, _staffSqlRepositoryMock.Object);
        }

        [Test(Author = "Lado Jikia", Description = "Updates background check record")]
        public async Task Update_Background_check_Accepted()
        {
            var staff = new Staff(_fixture.Create<int>());
            var check = new BackgroundCheck(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new UpdateBackgroundCheck
            {
                StaffId = staff.Id,
                CheckId = check.Id,
                Link = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _backgroundCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, Array.Empty<string>() ))
                .ReturnsAsync(check)
                .Verifiable();

            _backgroundCheckSqlRepositoryMock
                .Setup(x => x.UpdateAsync(It.Is<BackgroundCheck>(c => c.Id == request.CheckId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Accepted);

            Expression<Func<BackgroundCheck, bool>> match = f =>
                f.Id == request.CheckId && f.Staff.Id == request.StaffId && f.Link == request.Link &&
                f.Approver.Id == request.ApproverId && (int)f.CheckStatus == (int)request.CheckStatusId &&
                f.Date == request.Date;

            _backgroundCheckSqlRepositoryMock.Verify(f => f.UpdateAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "Staff not found")]
        public async Task Staff_Not_Found()
        {
            var request = new UpdateBackgroundCheck()
            {
                CheckId = _fixture.Create<int>(),
                StaffId = _fixture.Create<int>(),
                Link = _fixture.Create<string>(),
                ApproverId = _fixture.Create<int>(),
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Background check not found")]
        public async Task Background_Check_Not_Found()
        {
            var staff = new Staff(_fixture.Create<int>());

            var request = new UpdateBackgroundCheck
            {
                CheckId = _fixture.Create<int>(),
                StaffId = staff.Id,
                Link = _fixture.Create<string>(),
                ApproverId = _fixture.Create<int>(),
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _backgroundCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

       

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }


        [Test(Author = "Lado Jikia", Description = "Approver not found")]
        public async Task Approver_Not_Found()
        {
            var staff = new Staff(_fixture.Create<int>());
            var check = new BackgroundCheck(_fixture.Create<int>());

            var request = new UpdateBackgroundCheck
            {
                CheckId = check.Id,
                StaffId = staff.Id,
                Link = _fixture.Create<string>(),
                ApproverId = _fixture.Create<int>(),
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>()))
                .ReturnsAsync(staff)
                .Verifiable();

            _backgroundCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, Array.Empty<string>()))
                .ReturnsAsync(check)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Update_Background_Check_Validation_Failed()
        {
            var request = new UpdateBackgroundCheck();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
