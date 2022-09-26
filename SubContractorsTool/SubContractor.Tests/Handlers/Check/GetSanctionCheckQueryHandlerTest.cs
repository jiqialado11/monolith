using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Check.Queries.GetSanctionChecksQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractor.Tests.Handlers.Check
{
    [TestFixture]
    public class GetSanctionCheckQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetSanctionChecksQuery, Result<IList<GetSanctionChecksDto>>> _handler;

        private Mock<ISqlRepository<SanctionCheck, int>> _sanctionCheckSqlRepositoryMock;
        private Fixture _fixture;
        private GetSanctionChecksQueryValidator _validator;

        private int _subContractorId;
        private int _staffId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _staffId = _fixture.Create<int>();
            _subContractorId = _fixture.Create<int>();

            _fixture.Customizations.Add(new SanctionCheckSpecimenBuilder(_subContractorId, _staffId));

            _validator = new GetSanctionChecksQueryValidator();

            _sanctionCheckSqlRepositoryMock = MockRepository.Create<ISqlRepository<SanctionCheck, int>>();
            _handler = new GetSanctionChecksQueryHandler(_sanctionCheckSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of sanction checks for subcontractors")]
        public async Task Returns_Sanction_Check_For_SubContractors()
        {
            var sanctionChecks = _fixture.CreateMany<SanctionCheck>(10);

            var request = new GetSanctionChecksQuery
            {
                ParentId = _fixture.Create<int>(), ParentType = (int)ParentType.SubContractor
            };

            _sanctionCheckSqlRepositoryMock.Setup(x => x.FindAsync(s => s.SubContractor.Id == request.ParentId,
                    new string[] { nameof(SanctionCheck.SubContractor), nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(sanctionChecks);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(sanctionChecks.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of sanction checks for staff")]
        public async Task Returns_Sanction_Check_For_Staff()
        {
            var sanctionChecks = _fixture.CreateMany<SanctionCheck>(10);

            var request = new GetSanctionChecksQuery
            {
                ParentId = _fixture.Create<int>(), ParentType = (int)ParentType.Staff
            };

            _sanctionCheckSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => s.Staff.Id == request.ParentId, new string[] { nameof(SanctionCheck.Staff), nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(sanctionChecks);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(sanctionChecks.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found status in case there are no sanction checks")]
        public async Task Returns_Not_Found_For_SanctionChecks()
        {
            var sanctionChecks = _fixture.CreateMany<SanctionCheck>(0);

            var request = new GetSanctionChecksQuery
            {
                ParentId = _fixture.Create<int>(), ParentType = (int)_fixture.Create<ParentType>()
            };

            _sanctionCheckSqlRepositoryMock.Setup(x => x.FindAsync(s => s.SubContractor.Id == request.ParentId,
                    new string[] { nameof(SanctionCheck.SubContractor), nameof(SanctionCheck.Approver) }))
                .ReturnsAsync(sanctionChecks);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Sanction_Check_Validation_Failed()
        {
            var request = new GetSanctionChecksQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }

    public class SanctionCheckSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;
        private readonly int _subContractorId;
        private readonly int _staffId;

        public SanctionCheckSpecimenBuilder(int subContractorId, int staffId)
        {
            _subContractorId = subContractorId;
            _staffId = staffId;
            _fixture = new AutoFixture.Fixture();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(SanctionCheck))
            {
                return new SanctionCheck
                {
                    SubContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId),
                    Staff = new Staff(_staffId),
                    Comment = _fixture.Create<string>(),
                    Approver = new Staff(_fixture.Create<int>()),
                    CheckStatus = _fixture.Create<CheckStatus>(),
                    Date = _fixture.Create<DateTime>()
                };
            }

            return new NoSpecimen();
        }
    }
}
