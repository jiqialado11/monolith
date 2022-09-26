using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Compliance;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Compliance.Commands.CreateCompliance
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    [RequestInvalidateCache]
    public class CreateComplianceHandler : IRequestHandler<CreateCompliance, Result<int>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.Compliance.Compliance, int> _complianceSqlRepository;
        private readonly ISqlRepository<ComplianceFile, Guid> _complianceFileSqlRepository;
        private readonly ISqlRepository<ComplianceRating, int> _complianceRateSqlRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CreateComplianceHandler(
            ISqlRepository<SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Domain.Compliance.Compliance, int> complianceSqlRepository,
            ISqlRepository<ComplianceFile, Guid> complianceFileSqlRepository,
            IUnitOfWork unitOfWork,
            ISqlRepository<ComplianceRating, int> complianceRateSqlRepository
            )
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _complianceSqlRepository = complianceSqlRepository;
            _complianceFileSqlRepository = complianceFileSqlRepository;
            _unitOfWork = unitOfWork;
            _complianceRateSqlRepository = complianceRateSqlRepository;
        }

        public async Task<Result<int>> Handle(CreateCompliance request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId);
            if (subContractor == null)
            {
                return Result.NotFound<int>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var rating = await _complianceRateSqlRepository.GetAsync(x => x.Id == request.RatingId);
            if (rating == null)
            {
                return Result.NotFound<int>($"Compliance Rating wasn't found in database with provided identifier {request.RatingId}");
            }


            var file = await _complianceFileSqlRepository.GetAsync(x => x.Id == request.FileId, new string[]{nameof(ComplianceFile.Compliance)});

            if (file == null)
            {
                return Result.NotFound<int>($"Compliance File wasn't found in database with provided identifier {request.FileId}");
            }

            if (file.Compliance != null)
            {
                return Result.Fail<int>(ResultType.BadRequest,
                    $"Compliance File with identifier {file.Id} already linked with other compliance");
            }
            
            var compliance = new Domain.Compliance.Compliance();
            compliance.Create(request.Comment, request.ExpirationDate.Value, (ComplianceType)request.TypeId);
            compliance.AddFile(file);
            compliance.AddRating(rating);
            subContractor.AssignCompliance(compliance);

            await _complianceSqlRepository.AddAsync(compliance);

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Created, data: compliance.Id));
        }
    }
}
