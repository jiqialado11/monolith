using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Commands.UpdateSubContractorStatus
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class UpdateSubContractorStatusHandler : IRequestHandler<UpdateSubContractorStatus, Result<Unit>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSubContractorStatusHandler(
            ISqlRepository<SubContractor, int> subContractorRepository, 
            IUnitOfWork unitOfWork
            )
        {
            _subContractorRepository = subContractorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateSubContractorStatus request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorRepository.GetAsync(x => x.Id == request.SubContractorId);
            if (subContractor == null)
            {
                return Result.NotFound($"Subcontractor wasn't found in database with provided identifier {request.SubContractorStatusId}");
            }

            if (!Enum.IsDefined(typeof(SubContractorStatus), request.SubContractorStatusId))
            {
                return Result.NotFound($"Status wasn't found with provided identifier {request.SubContractorStatusId}");
            }

            subContractor.SubContractorStatus = (SubContractorStatus) request.SubContractorStatusId;

            await _subContractorRepository.UpdateAsync(subContractor);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
