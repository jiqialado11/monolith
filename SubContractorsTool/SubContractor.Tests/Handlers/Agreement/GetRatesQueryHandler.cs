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
using SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.SubContractor.Staff;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class GetRatesQueryHandler : BaseTestFixture
    {
        private IRequestHandler<GetRatesQuery, Result<IList<GetRatesDto>>> _handler;

        private Mock<IAddendaSqlRepository> _sqlRepositoryMock;
        private Mock<ISqlRepository<Rate, int>> _rateSqlRepository;

        private Fixture _fixture;
        private GetRatesQueryValidator _validator;

        private int _addendumId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new GetRatesQueryValidator();
            _addendumId = _fixture.Create<int>();

            _fixture.Customizations.Add(new RatesSpecimenBuilder(_addendumId));

            _sqlRepositoryMock =
                MockRepository.Create<IAddendaSqlRepository>();
            _rateSqlRepository =
                MockRepository.Create<ISqlRepository<Rate, int>>();
            _handler = new SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery.GetRatesQueryHandler(
                _sqlRepositoryMock.Object, _rateSqlRepository.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns list of rates with provided addendum identifier")]
        public async Task Returns_Rates_Ok()
        {
            var addendum = new Addendum(_addendumId);
            var rates = _fixture.CreateMany<Rate>();

            var request = new GetRatesQuery
            {
                AddendumId = _addendumId
            };

            _sqlRepositoryMock.Setup(x => x.GetAsync(request.AddendumId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(addendum)
                .Verifiable();

            _rateSqlRepository.Setup(x => x.FindAsync(x => x.Addendum.Id == request.AddendumId.Value,
                    new string[] { nameof(Rate.Addendum), nameof(Rate.Unit), nameof(Rate.Staff) }))
                .ReturnsAsync(rates)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(rates.Count(), result.Data.Count);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found for addendum")]
        public async Task Returns_Not_Found_For_Addendum()
        {
            var request = new GetRatesQuery
            {
                AddendumId = _fixture.Create<int>()
            };

            _sqlRepositoryMock.Setup(x => x.GetAsync(request.AddendumId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found for rates")]
        public async Task Returns_Not_Found_For_Rates()
        {
            var addendum = new Addendum(_addendumId);

            var request = new GetRatesQuery
            {
                AddendumId = _addendumId
            };

            _sqlRepositoryMock.Setup(x => x.GetAsync(request.AddendumId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(addendum)
                .Verifiable();

            _rateSqlRepository.Setup(x => x.FindAsync(x => x.Addendum.Id == request.AddendumId.Value,
                    new string[] { nameof(Rate.Addendum), nameof(Rate.Unit), nameof(Rate.Staff) }))
                .ReturnsAsync(()=> null)
                .Verifiable();

            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Rates_Validation_Failed()
        {
            var request = new GetRatesQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }
    }

    public class RatesSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;
        private readonly int _addendumId;

        public RatesSpecimenBuilder(int addendumId)
        {
            _addendumId = addendumId;
            _fixture = new Fixture();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(Rate))
            {
                var rate = new Rate(_fixture.Create<int>())
                {
                    Addendum = new Addendum(_addendumId),
                    Unit = new RateUnit(_fixture.Create<int>()),
                    Staff = new Staff(_fixture.Create<int>()),
                    FromDate = _fixture.Create<DateTime>(),
                    RateValue = _fixture.Create<decimal>(),
                    Description = _fixture.Create<string>(),
                    Name = _fixture.Create<string>(),
                    ToDate = _fixture.Create<DateTime>()

                };

                return rate;
            }

            return new NoSpecimen();
        }
    }
}
