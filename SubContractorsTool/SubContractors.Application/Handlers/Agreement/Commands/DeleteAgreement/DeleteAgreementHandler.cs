using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Agreement.Commands.DeleteAgreement
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteAgreementHandler : IRequestHandler<DeleteAgreement, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.Agreement.Agreement, int> _sqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAgreementHandler(ISqlRepository<Domain.Agreement.Agreement, int> sqlRepository, IUnitOfWork unitOfWork)
        {
            _sqlRepository = sqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteAgreement request, CancellationToken cancellationToken)
        {
            var agreement = await _sqlRepository.GetAsync(request.Id.Value, Array.Empty<string>() );
            if (agreement == null)
            {
                return Result.NotFound($"Agreement wasn't found in database with provided identifier {request.Id}");
            }

            await _sqlRepository.DeleteAsync(agreement.Id);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
