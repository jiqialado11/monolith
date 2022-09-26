using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Check.Commands.CreateBackgroundCheck;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractor.Tests.Handlers.Check
{
    [TestFixture]
    public class CreateBackgroundCheckHandlerTest : BaseTestFixture
    {
        private IRequestHandler<CreateBackgroundCheck, Result<int>> _handler;

        private Mock<ISqlRepository<BackgroundCheck, int>> _backgroundCheckSqlRepositoryMock;
        private Mock<ISqlRepository<Staff, int>> _staffSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private CreateBackgroundCheckValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new CreateBackgroundCheckValidator();

            _backgroundCheckSqlRepositoryMock = MockRepository.Create<ISqlRepository<BackgroundCheck, int>>();
            _staffSqlRepositoryMock = MockRepository.Create<ISqlRepository<Staff, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();

            _handler = new CreateBackgroundCheckHandler(_backgroundCheckSqlRepositoryMock.Object,
                _unitOfWorkMock.Object, _staffSqlRepositoryMock.Object);
        }


        [TearDown]
        public override void TearDown()
        {

        }

        [Test(Author = "Lado Jikia", Description = "Creates new background check record")]
        public async Task Create_New_Background_check_Accepted()
        {
            var staff = new Staff(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new CreateBackgroundCheck
            {
                StaffId = staff.Id,
                Link = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _backgroundCheckSqlRepositoryMock
                .Setup(x => x.AddAsync(It.Is<BackgroundCheck>(c => c.Staff.Id == request.StaffId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);

            Expression<Func<BackgroundCheck, bool>> match = f =>
                f.Staff.Id == request.StaffId && f.Link == request.Link && f.Approver.Id == request.ApproverId &&
                (int)f.CheckStatus == (int)request.CheckStatusId && f.Date == request.Date;

            _backgroundCheckSqlRepositoryMock.Verify(f => f.AddAsync(It.Is(match)), Times.Once);
        }


        [Test(Author = "Lado Jikia", Description = "Approver not found")]
        public async Task Approver_Not_Found()
        {
            var staff = new Staff(_fixture.Create<int>());
            var request = new CreateBackgroundCheck
            {
                StaffId = staff.Id,
                Link = _fixture.Create<string>(),
                ApproverId = _fixture.Create<int>(),
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>()))
                .ReturnsAsync(staff)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Staff not found")]
        public async Task Staff_Not_Found()
        {
            var approver = new Staff(_fixture.Create<int>());
            var request = new CreateBackgroundCheck
            {
                StaffId = _fixture.Create<int>(),
                Link = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };


            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();
            
            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.StaffId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Create_Background_Check_Validation_Failed()
        {
            var request = new CreateBackgroundCheck();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}