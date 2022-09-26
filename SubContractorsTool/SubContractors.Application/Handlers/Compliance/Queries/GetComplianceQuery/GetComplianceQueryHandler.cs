using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Application.Handlers.Compliance.Queries.GetComplianceListQuery;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetComplianceQueryHandler : IRequestHandler<GetComplianceQuery, Result<GetComplianceDto>>
    {
        private readonly ISqlRepository<Domain.Compliance.Compliance, int> _complianceSqlRepository;
        private readonly IMapper _mapper;

        public GetComplianceQueryHandler(ISqlRepository<Domain.Compliance.Compliance, int> complianceSqlRepository, IMapper mapper)
        {
            _complianceSqlRepository = complianceSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetComplianceDto>> Handle(GetComplianceQuery request, CancellationToken cancellationToken)
        {
            var compliance = await _complianceSqlRepository.GetAsync(request.Id.Value, new string[]
            {
                nameof(Domain.Compliance.Compliance.File), 
                nameof(Domain.Compliance.Compliance.Rating),
                nameof(Domain.Compliance.Compliance.SubContractor)
            });
            if (compliance == null)
            {
                return Result.NotFound<GetComplianceDto>($"Couldn't find compliance with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetComplianceDto>(compliance);

            return Result.Ok(value: result);

        }
    }
}
