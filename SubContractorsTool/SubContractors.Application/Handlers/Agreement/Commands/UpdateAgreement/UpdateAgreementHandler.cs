using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Budget;
using SubContractors.Domain.Common;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Commands.UpdateAgreement
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class UpdateAgreementHandler : IRequestHandler<UpdateAgreement, Result<Unit>>
    {
        private readonly IAgreementSqlRepository _agreementSqlRepository;
        private readonly ISqlRepository<Location, int> _locationSqlRepository;
        private readonly ISqlRepository<PaymentMethod, int> _paymentMethodSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.LegalEntity, int> _legalEntitySqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAgreementHandler(
            IAgreementSqlRepository agreementSqlRepository, 
            ISqlRepository<Location, int> locationSqlRepository, 
            ISqlRepository<PaymentMethod, int> paymentMethodSqlRepository, 
            ISqlRepository<Domain.SubContractor.LegalEntity, int> legalEntitySqlRepository, 
            IUnitOfWork unitOfWork
            )
        {
            _agreementSqlRepository = agreementSqlRepository;
            _locationSqlRepository = locationSqlRepository;
            _paymentMethodSqlRepository = paymentMethodSqlRepository;
            _legalEntitySqlRepository = legalEntitySqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateAgreement request, CancellationToken cancellationToken)
        {
            var agreement = await _agreementSqlRepository.GetAsync(request.Id.Value);
            if (agreement == null)
            {
                return Result.NotFound($"Agreement wasn't found in database with provided identifier {request.Id}");
            }

            if (agreement.BudgetOffice == null || agreement.BudgetOffice.Id != request.BudgetLocationId)
            {
                var budgetOffice = await _locationSqlRepository.GetAsync(request.BudgetLocationId.Value, Array.Empty<string>() );
                if (budgetOffice == null)
                {
                    return Result.NotFound($"Budget office wasn't found in database with provided identifier {request.BudgetLocationId.Value}");
                }

                agreement.AssignBudgetOffice(budgetOffice);
            }

            if (agreement.PaymentMethod == null || agreement.PaymentMethod.Id != request.PaymentMethodId)
            {
                var paymentMethod = await _paymentMethodSqlRepository.GetAsync(request.PaymentMethodId.Value, Array.Empty<string>() );
                if (paymentMethod == null)
                {
                    return Result.NotFound($"Payment method wasn't found in database with provided identifier {request.PaymentMethodId.Value}");
                }

                agreement.AssignPaymentMethod(paymentMethod);
            }

            if (agreement.LegalEntity == null || agreement.LegalEntity.Id != request.LegalEntityId)
            {
                var legalEntity = await _legalEntitySqlRepository.GetAsync(request.LegalEntityId.Value, Array.Empty<string>() );
                if (legalEntity == null)
                {
                    return Result.NotFound($"Legal entity wasn't found in database with provided identifier {request.LegalEntityId.Value}");
                }

                agreement.AssignLegalEntity(legalEntity);
            }

            agreement.Update(request.Title, request.AgreementUrl, request.StartDate.Value, request.EndDate.Value, request.Conditions);

            await _agreementSqlRepository.UpdateAsync(agreement);

            await _unitOfWork.SaveAsync();

            return  Result.Accepted();
        }
    }
}
