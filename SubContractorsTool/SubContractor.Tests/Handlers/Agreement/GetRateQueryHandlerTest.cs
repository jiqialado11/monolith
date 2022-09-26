using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Agreement.Queries.GetRateQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class GetRateQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetRateQuery, Result<GetRateDto>> _handler;

        private Mock<ISqlRepository<Rate, int>>
            _sqlRepository;

        private Fixture _fixture;
        private GetRateQueryValidator _validator;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new GetRateQueryValidator();

            _sqlRepository =
                MockRepository.Create<ISqlRepository<Rate, int>>();
            _handler = new GetRateQueryHandler(_sqlRepository.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns rate with provided identifier")]
        public async Task Returns_Rate_Ok()
        {
            var rate = new Rate(_fixture.Create<int>());

            var request = new GetRateQuery
            {
                RateId = rate.Id
            };

            _sqlRepository.Setup(x => x.GetAsync(request.RateId.Value,
                    new string[] { nameof(Rate.Addendum), nameof(Rate.Unit), nameof(Rate.Staff) }))
                .ReturnsAsync(rate);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(rate.Id, result.Data.Id);
            Assert.AreEqual(rate.Name, result.Data.Title);
            Assert.AreEqual(rate.Description, result.Data.Description);
        }

        [Test(Author = "Lado Jikia", Description = "Rate not found")]
        public async Task Rate_not_found()
        {
            var request = _fixture.Create<GetRateQuery>();

            _sqlRepository.Setup(x => x.GetAsync(request.RateId.Value,
                    new string[] { nameof(Rate.Addendum), nameof(Rate.Unit), nameof(Rate.Staff) }))
                .ReturnsAsync(()=> null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Rate_Validation_Failed()
        {
            var request = new GetRateQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }
}
