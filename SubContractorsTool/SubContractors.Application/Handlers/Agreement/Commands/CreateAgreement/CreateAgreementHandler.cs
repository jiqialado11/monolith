using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Commands.CreateAgreement
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class CreateAgreementHandler : IRequestHandler<CreateAgreement, Result<int>>
    {
        private readonly IAgreementSqlRepository _agreementSqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Location, int> _locationSqlRepository;
        private readonly ISqlRepository<PaymentMethod, int> _paymentMethodSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.LegalEntity, int> _legalEntitySqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAgreementHandler(
            IAgreementSqlRepository agreementSqlRepository, 
            ISqlRepository<SubContractor, int> subContractorSqlRepository, 
            ISqlRepository<Location, int> locationSqlRepository, 
            ISqlRepository<PaymentMethod, int> paymentMethodSqlRepository,
            ISqlRepository<Domain.SubContractor.LegalEntity, int> legalEntitySqlRepository, 
            IUnitOfWork unitOfWork)
        {
            _agreementSqlRepository = agreementSqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _locationSqlRepository = locationSqlRepository;
            _paymentMethodSqlRepository = paymentMethodSqlRepository;
            _legalEntitySqlRepository = legalEntitySqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateAgreement request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(request.SubContractorId.Value, new string[]{});
            if (subContractor == null)
            {
                return Result.NotFound<int>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var office = await _locationSqlRepository.GetAsync(request.BudgetLocationId.Value, Array.Empty<string>() );
            if (office == null)
            {
                return Result.NotFound<int>($"Budget Office wasn't found in database with provided identifier {request.BudgetLocationId}");
            }

            var paymentMethod = await _paymentMethodSqlRepository.GetAsync(request.PaymentMethodId.Value, Array.Empty<string>() );
            if (paymentMethod == null)
            {
                return Result.NotFound<int>($"Payment Method wasn't found in database with provided identifier {request.PaymentMethodId}");
            }

            var legalEntity = await _legalEntitySqlRepository.GetAsync(request.LegalEntityId.Value, Array.Empty<string>() );
            if (legalEntity == null)
            {
                return Result.NotFound<int>($"Legal Entity wasn't found in database with provided identifier {request.LegalEntityId}");
            }


            var agreement = new Domain.Agreement.Agreement();
            agreement.Create(request.Title, request.AgreementUrl, request.StartDate.Value, request.EndDate.Value,
                request.Conditions);

            agreement.AssignBudgetOffice(office);
            agreement.AssignLegalEntity(legalEntity);
            agreement.AssignPaymentMethod(paymentMethod);
            subContractor.AssignAgreement(agreement);

            await _agreementSqlRepository.AddAsync(agreement);
            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Created, data: agreement.Id);
        }
    }
}