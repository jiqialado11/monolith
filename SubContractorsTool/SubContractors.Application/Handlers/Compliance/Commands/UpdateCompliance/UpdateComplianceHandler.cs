using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Commands.UpdateCompliance
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class UpdateComplianceHandler : IRequestHandler<UpdateCompliance, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.Compliance.Compliance, int> _complianceSqlRepository;
        private readonly ISqlRepository<ComplianceFile, Guid> _complianceFileSqlRepository;
        private readonly ISqlRepository<ComplianceRating, int> _complianceRateSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateComplianceHandler(
            ISqlRepository<Domain.Compliance.Compliance, int> complianceSqlRepository, 
            ISqlRepository<ComplianceFile, Guid> complianceFileSqlRepository, 
            ISqlRepository<ComplianceRating, int> complianceRateSqlRepository, 
            IUnitOfWork unitOfWork)
        {
            _complianceSqlRepository = complianceSqlRepository;
            _complianceFileSqlRepository = complianceFileSqlRepository;
            _complianceRateSqlRepository = complianceRateSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(UpdateCompliance request, CancellationToken cancellationToken)
        {

            var compliance = await _complianceSqlRepository.GetAsync(x => x.Id == request.Id,
                new string[]
                    { nameof(Domain.Compliance.Compliance.File), nameof(Domain.Compliance.Compliance.Rating) });
            if (compliance == null)
            {
                return Result.NotFound($"Compliance wasn't found in database with provided identifier {request.Id.Value}");
            }

            var rating = await _complianceRateSqlRepository.GetAsync(x => x.Id == request.RatingId);
            if (rating == null)
            {
                return Result.NotFound($"Compliance Rating wasn't found in database with provided identifier {request.RatingId}");
            }

            if (compliance.File == null || compliance.File.Id != request.FileId)
            {
                if (compliance.File != null)
                {
                    await _complianceFileSqlRepository.DeleteAsync(compliance.File.Id);
                }
                var file = await _complianceFileSqlRepository.GetAsync(x => x.Id == request.FileId, new string[]{ nameof(ComplianceFile.Compliance) });
                if (file == null)
                {
                    return Result.NotFound($"Compliance File wasn't found in database with provided identifier {request.FileId}");
                }

                if (file.Compliance != null && file.Compliance.Id != compliance.Id)
                {
                    return Result.Fail(ResultType.BadRequest,
                        $"Compliance File with identifier {file.Id} already linked with other compliance");
                }
                compliance.AddFile(file);
            }
               
             
            

            compliance.Update(request.Comment, request.ExpirationDate.Value, (ComplianceType)request.TypeId);
            compliance.AddRating(rating);

            await _complianceSqlRepository.UpdateAsync(compliance);

            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Accepted);
        }
    }
}
