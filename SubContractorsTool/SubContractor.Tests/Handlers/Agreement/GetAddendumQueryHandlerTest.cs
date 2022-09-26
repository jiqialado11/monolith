using AutoFixture;
using AutoFixture.Kernel;
using MediatR;
using Moq;
using NUnit.Framework;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery;
using SubContractors.Common;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Domain.Project;
using SubContractors.Domain.SubContractor.Staff;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class GetAddendumQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetAddendumQuery, Result<GetAddendumDto>> _handler;

        private Mock<IAddendaSqlRepository>
            _addendaSqlRepository;

        private Fixture _fixture;
        private GetAddendumQueryValidator _validator;

        private  int _addendumId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new GetAddendumQueryValidator();
            _addendumId = _fixture.Create<int>();

            _fixture.Customizations.Add(new AddendumSpecimenBuilder(_addendumId));

            _addendaSqlRepository =
                MockRepository.Create<IAddendaSqlRepository>();

            _handler = new GetAddendumQueryHandler(_addendaSqlRepository.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns addendum with provided identifier")]
        public async Task Returns_Addendum_Ok()
        {
            var addendum = _fixture.Create<Addendum>();

            var request = new GetAddendumQuery
            {
                Id = addendum.Id
            };

            _addendaSqlRepository.Setup(x => x.GetAsync(request.Id.Value))
                .ReturnsAsync(addendum);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(addendum.Id, result.Data.Id);
            Assert.AreEqual(addendum.Title, result.Data.Title);
            Assert.AreEqual(addendum.StartDate, result.Data.StartDate);
            Assert.AreEqual(addendum.EndDate, result.Data.EndDate);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found for addendum")]
        public async Task Returns_Not_Found_For_Addendum()
        {

            var request = new GetAddendumQuery()
            {
                Id = _fixture.Create<int>()
            };

            _addendaSqlRepository.Setup(x => x.GetAsync(request.Id.Value))
                .ReturnsAsync(() => null);


            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Addendum_Validation_Failed()
        {
            var request = new GetAddendumQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }


    }

    public class AddendumSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;
        private readonly int _addendumId;
        public AddendumSpecimenBuilder(int addendumId)
        {
            _fixture = new Fixture();
            _addendumId = addendumId;
        }

        public object Create(object request, ISpecimenContext context)
        {
           
            if (request is Type type && type == typeof(Addendum))
            {
                var addendum = new Addendum(_addendumId)
                {
                    Agreement = new SubContractors.Domain.Agreement.Agreement(_fixture.Create<int>()),
                    Projects = new List<Project>{new Project(_fixture.Create<Guid>())},
                    Invoices = new List<SubContractors.Domain.Invoice.Invoice> { new(_fixture.Create<int>()) },
                    Staffs = new List<Staff> { new(_fixture.Create<int>()) },
                    Comment = _fixture.Create<string>(),
                    Currency = _fixture.Create<Currency>(),
                    DocumentUrl = _fixture.Create<string>(),
                    EndDate = _fixture.Create<DateTime>(),
                    IsRateForNonBillableProjects = _fixture.Create<bool>(),
                    PaymentTerm = _fixture.Create<PaymentTerm>(),
                    PaymentTermInDays = _fixture.Create<int>(),
                    Rates = new List<Rate> { new(_fixture.Create<int>()) },
                    StartDate = _fixture.Create<DateTime>(),
                    Title = _fixture.Create<string>()

                };

                return addendum;
            }


            return new NoSpecimen();

        }

    }
}
