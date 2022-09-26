using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Commands.CreateAddendum
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class CreateAddendumHandler : IRequestHandler<CreateAddendum, Result<int>>
    {
        private readonly IAgreementSqlRepository _agreementSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectsSqlRepository;
        private readonly ISqlRepository<PaymentTerm, int> _paymentTermsSqlRepository;
        private readonly ISqlRepository<Currency, int> _currenciesSqlRepository;
        private readonly IAddendaSqlRepository _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateAddendumHandler(
            ISqlRepository<Domain.Project.Project, Guid> projectsSqlRepository, 
            ISqlRepository<PaymentTerm, int> paymentTermsSqlRepository, 
            ISqlRepository<Currency, int> currenciesSqlRepository,  
            IUnitOfWork unitOfWork, 
            IAgreementSqlRepository agreementSqlRepository,
            IAddendaSqlRepository sqlRepository)
        {
            _projectsSqlRepository = projectsSqlRepository;
            _paymentTermsSqlRepository = paymentTermsSqlRepository;
            _currenciesSqlRepository = currenciesSqlRepository;
            _unitOfWork = unitOfWork;
            _agreementSqlRepository = agreementSqlRepository;
            _sqlRepository = sqlRepository;
        }

        public async Task<Result<int>> Handle(CreateAddendum request, CancellationToken cancellationToken)
        {
            var agreement = await _agreementSqlRepository.GetAsync(x => x.Id == request.AgreementId, new string[]{});
            if (agreement == null)
            {
                return Result.NotFound<int>($"Agreement wasn't found in database with provided identifier {request.AgreementId}");
            }
            
            var paymentTerm = await _paymentTermsSqlRepository.GetAsync(x => x.Id == request.PaymentTermId, new string[]{});
            if (paymentTerm == null)
            {
                return Result.NotFound<int>($"PaymentTerm wasn't found in database with provided identifier {request.PaymentTermId}");
            }

            var currency = await _currenciesSqlRepository.GetAsync(x => x.Id == request.CurrencyId, new string[]{});
            if (currency == null)
            {
                return Result.NotFound<int>($"Currency wasn't found in database with provided identifier {request.CurrencyId}");
            }

            var projects = new List<Domain.Project.Project>();

            foreach (var projectId in request.ProjectIds)
            {
                var project = await _projectsSqlRepository.GetAsync(projectId, Array.Empty<string>() );
                if (project == null)
                {
                    return Result.NotFound<int>($"Project wasn't found in database with provided identifier {projectId}");
                }

                projects.Add(project);

            }

            var addendum = new Addendum();
            addendum.Create(request.Title, request.Url, request.StartDate.Value, request.EndDate.Value, 
                request.Comment, request.PaymentTermInDays, request.IsForNonBillableProjects);

            addendum.AssignCurrency(currency);
            addendum.AssignPaymentTerm(paymentTerm);
            addendum.AssignToAgreement(agreement);
            foreach (var project in projects)
            {
                addendum.AddProject(project);
            }

         
            await _sqlRepository.AddAsync(addendum);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Created, data: addendum.Id));
        }
    }
}