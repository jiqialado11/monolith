using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Check.Commands.UpdateSanctionCheck;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractor.Tests.Handlers.Check
{
    [TestFixture]
    public class UpdateSanctionCheckHandlerTest : BaseTestFixture
    {
        private IRequestHandler<UpdateSanctionCheck, Result<Unit>> _handler;

        private Mock<ISqlRepository<SanctionCheck, int>> _sanctionCheckSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>> _subcontractorSqlRepositoryMock;
        private Mock<ISqlRepository<Staff, int>> _staffSqlRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Fixture _fixture;

        private UpdateSanctionCheckValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new UpdateSanctionCheckValidator();

            _sanctionCheckSqlRepositoryMock = MockRepository.Create<ISqlRepository<SanctionCheck, int>>();
            _subcontractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _unitOfWorkMock = MockRepository.Create<IUnitOfWork>();
            _staffSqlRepositoryMock = MockRepository.Create<ISqlRepository<Staff, int>>();

            _handler = new UpdateSanctionCheckHandler(_sanctionCheckSqlRepositoryMock.Object,
                _subcontractorSqlRepositoryMock.Object, _unitOfWorkMock.Object, _staffSqlRepositoryMock.Object);
        }
        
        [TearDown]
        public override void TearDown()
        {

        }

        [Test(Author = "Lado Jikia", Description = "Updates sanction check record for subcontractor")]
        public async Task Update_Sanction_check_For_SubContractor_Accepted()
        {
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());
            var check = new SanctionCheck(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new UpdateSanctionCheck()
            {
                ParentId = subContractor.Id,
                ParentType = (int)ParentType.SubContractor,
                CheckId = check.Id,
                Comment = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _subcontractorSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.ParentId, Array.Empty<string>() ))
                .ReturnsAsync(subContractor)
                .Verifiable();

            _sanctionCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, new string[] { nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(check)
                .Verifiable();

            _sanctionCheckSqlRepositoryMock
                .Setup(x => x.UpdateAsync(It.Is<SanctionCheck>(c => c.Id == request.CheckId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Accepted);

            Expression<Func<SanctionCheck, bool>> match = f =>
                f.Id == request.CheckId && f.SubContractor.Id == request.ParentId && f.Comment == request.Comment &&
                f.Approver.Id == request.ApproverId && (int)f.CheckStatus == (int)request.CheckStatusId &&
                f.Date == request.Date;

            _sanctionCheckSqlRepositoryMock.Verify(f => f.UpdateAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "Updates sanction check record for staff")]
        public async Task Update_Sanction_check_For_Staff_Accepted()
        {
            var staff = new Staff(_fixture.Create<int>());
            var check = new SanctionCheck(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new UpdateSanctionCheck()
            {
                ParentId = staff.Id,
                ParentType = (int)ParentType.Staff,
                CheckId = check.Id,
                Comment = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };


            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(approver)
                .Verifiable();

            _staffSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.ParentId, Array.Empty<string>() ))
                .ReturnsAsync(staff)
                .Verifiable();

            _sanctionCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, new string[] { nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(check)
                .Verifiable();

            _sanctionCheckSqlRepositoryMock
                .Setup(x => x.UpdateAsync(It.Is<SanctionCheck>(c => c.Id == request.CheckId)))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.FromResult(1)).Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.Accepted);

            Expression<Func<SanctionCheck, bool>> match = f =>
                f.Id == request.CheckId && f.Staff.Id == request.ParentId && f.Comment == request.Comment &&
                f.Approver.Id == request.ApproverId && (int)f.CheckStatus == (int)request.CheckStatusId &&
                f.Date == request.Date;

            _sanctionCheckSqlRepositoryMock.Verify(f => f.UpdateAsync(It.Is(match)), Times.Once);
        }

        [Test(Author = "Lado Jikia", Description = "SubContractor not found")]
        public async Task SubContractor_Not_Found()
        {
            var check = new SanctionCheck(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new UpdateSanctionCheck()
            {
                CheckId = check.Id,
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

            _sanctionCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, new string[] { nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(check)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Staff not found")]
        public async Task Staff_Not_Found()
        {
            var check = new SanctionCheck(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new UpdateSanctionCheck()
            {
                CheckId = check.Id,
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

            _sanctionCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, new string[] { nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(check)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Sanction check not found ")]
        public async Task Sanction_Check_Not_Found()
        {
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_fixture.Create<int>());

            var request = new UpdateSanctionCheck()
            {
                CheckId = _fixture.Create<int>(),
                ParentId = subContractor.Id,
                ParentType = (int)ParentType.SubContractor,
                Comment = _fixture.Create<string>(),
                ApproverId = _fixture.Create<int>(),
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };

            _sanctionCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, new string[] { nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(() => null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Approver not found ")]
        public async Task Approver_Not_Found()
        {
            var check = new SanctionCheck(_fixture.Create<int>());
            var approver = new Staff(_fixture.Create<int>());

            var request = new UpdateSanctionCheck()
            {
                CheckId = check.Id,
                ParentId = _fixture.Create<int>(),
                ParentType = (int)ParentType.Staff,
                Comment = _fixture.Create<string>(),
                ApproverId = approver.Id,
                CheckStatusId = (int)CheckStatus.Passed,
                Date = _fixture.Create<DateTime>()
            };


            _staffSqlRepositoryMock.Setup(x => x.GetAsync(request.ApproverId.Value, Array.Empty<string>()))
                .ReturnsAsync(() => null)
                .Verifiable();

            
            _sanctionCheckSqlRepositoryMock.Setup(x => x.GetAsync(s => s.Id == request.CheckId, new string[] { nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(check)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(result.StatusCode, (int)ResultType.NotFound);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Update_Sanction_Check_Validation_Failed()
        {
            var request = new UpdateSanctionCheck();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
