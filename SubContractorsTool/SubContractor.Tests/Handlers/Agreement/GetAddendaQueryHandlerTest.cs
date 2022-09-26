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
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendaQuery;
using SubContractors.Application.Handlers.Agreement.Queries.GetAddendumQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Domain.Invoice;
using SubContractors.Domain.Project;
using SubContractors.Domain.SubContractor;
using SubContractors.Domain.SubContractor.Staff;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractor.Tests.Handlers.Agreement
{
    [TestFixture]
    public class GetAddendaQueryHandlerTest : BaseTestFixture
    {
        private IRequestHandler<GetAddendaQuery, Result<IList<GetAddendumDto>>> _handler;

        private Mock<IAddendaSqlRepository>
            _sqlRepository;
        private Mock<ISqlRepository<SubContractors.Domain.Agreement.Agreement, int>> _agreementSqlRepositoryMock;
        private Mock<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>> _subContractorSqlRepositoryMock;

        private Fixture _fixture;
        private GetAddendaQueryValidator _validator;

        private int _subContractorId;
        private int _staffId;
        private Guid _projectId;
        private int _agreementId;
        private int _invoiceId;
        
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _fixture = new Fixture();
            _validator = new GetAddendaQueryValidator();

            _subContractorId = _fixture.Create<int>();
            _projectId = _fixture.Create<Guid>();
            _invoiceId = _fixture.Create<int>();
            _agreementId = _fixture.Create<int>();
            _staffId = _fixture.Create<int>();

            _fixture.Customizations.Add(new AddendaSpecimenBuilder(_subContractorId, _agreementId, _invoiceId, _projectId, _staffId));

            _sqlRepository =
                MockRepository.Create<IAddendaSqlRepository>();
            _agreementSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.Agreement.Agreement, int>>();
            _subContractorSqlRepositoryMock = MockRepository.Create<ISqlRepository<SubContractors.Domain.SubContractor.SubContractor, int>>();
            _handler = new GetAddendaQueryHandler(_sqlRepository.Object, _agreementSqlRepositoryMock.Object, 
                _subContractorSqlRepositoryMock.Object, Mapper);
        }

        [Test(Author = "Lado Jikia", Description = "Returns addenda with provided subcontractor identifier")]
        public async Task Returns_Addenda_Ok()
        {
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId);
            var agreements = _fixture.CreateMany<SubContractors.Domain.Agreement.Agreement>(1);
            var addenda = _fixture.CreateMany<Addendum>(10);

            var request = new GetAddendaQuery
            {
                SubContractorId = _subContractorId
            };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(subContractor);

            _agreementSqlRepositoryMock.Setup(x => x.FindAsync(s => s.SubContractor.Id == request.SubContractorId.Value,
                    new string[] { nameof(SubContractors.Domain.Agreement.Agreement.SubContractor), nameof(SubContractors.Domain.Agreement.Agreement.LegalEntity)}))
                .ReturnsAsync(agreements);


            _sqlRepository.Setup(x => x.FindAsync( x=> agreements.Select(a => a.Id).ToList()
                        .Contains(x.Agreement.Id)))
                .ReturnsAsync(addenda);
            

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(ResultType.Ok, result.Type);
            Assert.AreEqual(addenda.Count(), result.Data.Count);

        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found for subcontractor")]
        public async Task Returns_Not_Found_For_SubContractor()
        {
            
            var request = new GetAddendaQuery
            {
                SubContractorId = _subContractorId
            };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(() => null);

            
            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found for agreements")]
        public async Task Returns_Not_Found_For_Agreements()
        {
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId);
            var agreements = _fixture.CreateMany<SubContractors.Domain.Agreement.Agreement>(0);

            var request = new GetAddendaQuery
            {
                SubContractorId = _subContractorId
            };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(subContractor);

            _agreementSqlRepositoryMock.Setup(x => x.FindAsync(s => s.SubContractor.Id == request.SubContractorId.Value,
                    new string[] { nameof(SubContractors.Domain.Agreement.Agreement.SubContractor), nameof(SubContractors.Domain.Agreement.Agreement.LegalEntity) }))
                .ReturnsAsync(agreements);

            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Returns Not found for addenda")]
        public async Task Returns_Not_Found_For_Addenda()
        {
            var subContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId);
            var agreements = _fixture.CreateMany<SubContractors.Domain.Agreement.Agreement>(1);
            var addenda = _fixture.CreateMany<Addendum>(0);


            var request = new GetAddendaQuery
            {
                SubContractorId = _subContractorId
            };

            _subContractorSqlRepositoryMock.Setup(x => x.GetAsync(request.SubContractorId.Value,
                    Array.Empty<string>() ))
                .ReturnsAsync(subContractor);

            _agreementSqlRepositoryMock.Setup(x => x.FindAsync(s => s.SubContractor.Id == request.SubContractorId.Value,
                    new string[] { nameof(SubContractors.Domain.Agreement.Agreement.SubContractor), nameof(SubContractors.Domain.Agreement.Agreement.LegalEntity) }))
                .ReturnsAsync(agreements);

            _sqlRepository.Setup(x => x.FindAsync(x => agreements.Select(a => a.Id).ToList()
                        .Contains(x.Agreement.Id)))
                .ReturnsAsync(addenda);

            var result = await _handler.Handle(request, CancellationToken.None);


            Assert.IsTrue(!result.IsSuccess);
            Assert.AreEqual(ResultType.NotFound, result.Type);
            Assert.Null(result.Data);
        }

        [Test(Author = "Lado Jikia", Description = "Validation failure")]
        public async Task Get_Addenda_Validation_Failed()
        {
            var request = new GetAddendaQuery();

            var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);

            Assert.IsTrue(!validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count > 0);
        }

    }

    public class AddendaSpecimenBuilder : ISpecimenBuilder
    {
        private readonly Fixture _fixture;
        private readonly int _agreementId;
        private readonly int _subContractorId;
        private readonly int _invoiceId;
        private readonly Guid _projectId;
        private readonly int _staffId;

        public AddendaSpecimenBuilder(int subContractorId, int agreementId, int invoiceId, Guid projectId, int staffId)
        {
            _subContractorId = subContractorId;
            _agreementId = agreementId;
            _invoiceId = invoiceId;
            _projectId = projectId;
            _staffId = staffId;
            _fixture = new Fixture();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is Type type && type == typeof(SubContractors.Domain.Agreement.Agreement))
            {
                var agreement = new SubContractors.Domain.Agreement.Agreement(_agreementId)
                {
                    SubContractor = new SubContractors.Domain.SubContractor.SubContractor(_subContractorId),
                    LegalEntity = _fixture.Create<LegalEntity>(),
                    Title = _fixture.Create<string>(),
                };

                return agreement;
            }

            if (request is Type type2 && type2 == typeof(Addendum))
            {
                var addendum = new Addendum
                {
                    Agreement = new SubContractors.Domain.Agreement.Agreement(_agreementId),
                    Projects = new List<Project>{ new Project(_projectId) },
                    Invoices = new List<SubContractors.Domain.Invoice.Invoice> { new(_invoiceId) },
                    Staffs = new List<Staff> { new(_staffId) },
                    Comment = _fixture.Create<string>(),
                    Currency = _fixture.Create<Currency>(),
                    DocumentUrl = _fixture.Create<string>(),
                    EndDate = _fixture.Create<DateTime>(),
                    IsRateForNonBillableProjects = _fixture.Create<bool>(),
                    PaymentTerm = _fixture.Create<PaymentTerm>(),
                    PaymentTermInDays = _fixture.Create<int>(),
                    Rates = new List<Rate>{new(_fixture.Create<int>()) },
                    StartDate = _fixture.Create<DateTime>(),
                    Title = _fixture.Create<string>()

                };

                return addendum;
            }


            return new NoSpecimen();

        }

    }
}
