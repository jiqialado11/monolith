using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetComplianceListQueryHandler : IRequestHandler<GetComplianceListQuery, Result<IList<GetComplianceDto>>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.Compliance.Compliance, int> _complianceSqlRepository;
        private readonly IMapper _mapper;

        public GetComplianceListQueryHandler(
            ISqlRepository<Domain.Compliance.Compliance, int> complianceSqlRepository, 
            ISqlRepository<SubContractor, int> subContractorSqlRepository, 
            IMapper mapper)
        {
            _complianceSqlRepository = complianceSqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetComplianceDto>>> Handle(GetComplianceListQuery request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(request.SubContractorId.Value);
            if (subContractor == null)
            {
                return Result.NotFound<IList<GetComplianceDto>>($"Couldn't find subContractor with provided identifier {request.SubContractorId.Value}");
            }

            var compliances = await _complianceSqlRepository.FindAsync(
                x => x.SubContractor.Id == request.SubContractorId.Value,
                new[] { nameof(Domain.Compliance.Compliance.File), nameof(Domain.Compliance.Compliance.Rating) });

            if (compliances == null || !compliances.Any())
            {
                return Result.NotFound<IList<GetComplianceDto>>($"SubContractor with identifier {request.SubContractorId.Value} doesn't have compliances");
            }

            IList<GetComplianceDto> result = compliances.Select(x => _mapper.Map<GetComplianceDto>(x))
                .ToList();

            return Result.Ok(value: result);
        }
    }
}
