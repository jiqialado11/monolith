using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Check.Commands.CreateSanctionCheck;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractor.Tests.Handlers.Check
{
    [TestFixture]
    public class CreateSanctionCheckHandlerTest : BaseTestFixture
    {
        private IRequestHandler<CreateSanctionCheck, Result<int>> _handler;

        private Mock<ISqlRepository<SanctionCheck, int>> _sanctionCheckSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>> _subcontractorSqlRepositoryMock;
        private Mock<ISqlRepository<Staff, int>> _staffSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private CreateSanctionCheckValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new CreateSanctionCheckValidator();

            _sanctionCheckSqlRepositoryMock = MockRepository.Create<ISqlRepository<SanctionCheck, int>>();
            _subcontractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _staffSqlRepositoryMock = MockRepository.Create<ISqlRepository<Staff, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();

            _handler = new CreateSanctionCheckHandler(_sanctionCheckSqlRepositoryMock.Object, _unitOfWorkMock.Object,
                _subcontractorSqlRepositoryMock.Object, _staffSqlRepositoryMock.Object);
        }

        [Test(Author = "Lado Jikia", Description = "Creates new sanction check record for subContractor")]
        public async Task Create_New_Sanction_check_For_SubContractor_Accepted()
        {
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new CreateSanctionCheck
            {
                ParentId = subContractor.Id,
                ParentType = (int)ParentType.SubContractor,
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>(),
                Comment = _fixture.Create<string>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _subcontractorSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.ParentId, Array.Empty<string>() ))
                .ReturnsAsync(subContractor)
                .Verifiable();

            _sanctionCheckSqlRepositoryMock
                .Setup(x => x.AddAsync(It.Is<SanctionCheck>(c => c.SubContractor.Id == request.ParentId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);

            Expression<Func<SanctionCheck, bool>> match = f =>
                f.SubContractor.Id == request.ParentId && f.Comment == request.Comment &&
                f.Approver.Id == request.ApproverId && (int)f.CheckStatus == (int)request.CheckStatusId &&
                f.Date == request.Date;

            _sanctionCheckSqlRepositoryMock.Verify(f => f.AddAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "Creates new sanction check record for staff")]
        public async Task Create_New_Sanction_check_For_Staff_Accepted()
        {
            var staff = new Staff(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new CreateSanctionCheck
            {
                ParentId = staff.Id,
                ParentType = (int)ParentType.Staff,
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>(),
                Comment = _fixture.Create<string>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.ParentId, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _sanctionCheckSqlRepositoryMock
                .Setup(x => x.AddAsync(It.Is<SanctionCheck>(c => c.Staff.Id == request.ParentId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Created);

            Expression<Func<SanctionCheck, bool>> match = f =>
                f.Staff.Id == request.ParentId && f.Comment == request.Comment && f.Approver.Id == request.ApproverId &&
                (int)f.CheckStatus == (int)request.CheckStatusId && f.Date == request.Date;

            _sanctionCheckSqlRepositoryMock.Verify(f => f.AddAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "Approver not found")]
        public async Task Approver_Not_Found()
        {
            var request = new CreateSanctionCheck
            {
                ParentId = _fixture.Create<int>(),
                ParentType = (int)ParentType.SubContractor,
                Comment = _fixture.Create<string>(),
                ApproverId = _fixture.Create<int>(),
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "SubContractor not found")]
        public async Task SubContractor_Not_Found()
        {
            var approver = new Staff(_fixture.Create<int>());
            var request = new CreateSanctionCheck
            {
                ParentId = _fixture.Create<int>(),
                ParentType = (int)ParentType.SubContractor,
                Comment = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _subcontractorSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.ParentId, Array.Empty<string>() ))
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
            var request = new CreateSanctionCheck
            {
                ParentId = _fixture.Create<int>(),
                ParentType = (int)ParentType.Staff,
                Comment = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.ParentId, Array.Empty<string>() ))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Create_Sanction_Check_Validation_Failed()
        {
            var request = new CreateSanctionCheck();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
