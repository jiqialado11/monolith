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
using SubContractors.Application.Handlers.Check.Queries.GetBackgroundChecksQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Check;
using SubContractors.Domain.SubContractor.Staff;
using DateTime = System.DateTime;

namespace SubContractor.Tests.Handlers.Check
{
    [TestFixture]
    public class GetBackgroundCheckQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetBackgroundChecksQuery, Result<IList<GetBackgroundChecksDto>>> _handler;

        private Mock<ISqlRepository<BackgroundCheck, int>> _backgroundCheckSqlRepositoryMock;
        private Fixture _fixture;
        private GetBackgroundChecksQueryValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _fixture.Customizations.Add(new BackgroundCheckSpecimenBuilder());
            _validator = new GetBackgroundChecksQueryValidator();

            _backgroundCheckSqlRepositoryMock = MockRepository.Create<ISqlRepository<BackgroundCheck, int>>();
            _handler = new GetBackgroundChecksQueryHandler(_backgroundCheckSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of background checks")]
        public async Task Returns_Background_Checks()
        {
            var backgroundChecks = _fixture.CreateMany<BackgroundCheck>(10);

            var request = new GetBackgroundChecksQuery { StaffId = _fixture.Create<int>() };

            _backgroundCheckSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => s.Staff.Id == request.StaffId, new string[]{ nameof(BackgroundCheck.Approver)} ))
                .ReturnsAsync(backgroundChecks);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(backgroundChecks.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found status in case there are no background checks")]
        public async Task Returns_Not_Found_For_BackgroundChecks()
        {
            var backgroundChecks = _fixture.CreateMany<BackgroundCheck>(0);

            var request = new GetBackgroundChecksQuery { StaffId = _fixture.Create<int>() };

            _backgroundCheckSqlRepositoryMock.Setup(x =>
                    x.FindAsync(s => s.Staff.Id == request.StaffId, new string[] { nameof(BackgroundCheck.Approver) }))
                .ReturnsAsync(backgroundChecks);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Background_Check_Validation_Failed()
        {
            var request = new GetBackgroundChecksQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }

    public class BackgroundCheckSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;

        public BackgroundCheckSpecimenBuilder()
        {
            _fixture = new AutoFixture.Fixture();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(BackgroundCheck))
            {
                return new BackgroundCheck
                {
                    Staff = new Staff(_fixture.Create<int>()),
                    Link = _fixture.Create<string>(),
                    Approver = new Staff(_fixture.Create<int>()),
                    CheckStatus = _fixture.Create<CheckStatus>(),
                    Date = _fixture.Create<DateTime>()
                };
            }

            return new NoSpecimen();
        }
    }
}
