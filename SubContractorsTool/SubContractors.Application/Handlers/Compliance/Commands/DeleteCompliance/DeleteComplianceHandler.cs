using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Commands.DeleteCompliance
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class DeleteComplianceHandler : IRequestHandler<DeleteCompliance, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.Compliance.Compliance, int> _complianceSqlRepository;
        private readonly ISqlRepository<ComplianceFile, Guid> _complianceFileSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteComplianceHandler(
            ISqlRepository<Domain.Compliance.Compliance, int> complianceSqlRepository,
            ISqlRepository<ComplianceFile, Guid> complianceFileSqlRepository,
            IUnitOfWork unitOfWork
            )
        {
            _complianceSqlRepository = complianceSqlRepository;
            _complianceFileSqlRepository = complianceFileSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteCompliance request, CancellationToken cancellationToken)
        {
            var compliance = await _complianceSqlRepository.GetAsync(x => x.Id == request.Id,
                new string[]
                    { nameof(Domain.Compliance.Compliance.File), nameof(Domain.Compliance.Compliance.Rating) });
            if (compliance == null)
            {
                return Result.NotFound($"Compliance wasn't found in database with provided identifier {request.Id.Value}");
            }

            var file = compliance.File;

            if (file != null)
            {
                await _complianceFileSqlRepository.DeleteAsync(file.Id);
            }

            await _complianceSqlRepository.DeleteAsync(request.Id.Value);


            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Ok);
        }
    }
}
