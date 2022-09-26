using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceRatingsQuery
{
    [RequestLogging]
    [RequestCashing]
    public class GetComplianceRatingsQueryHandler : IRequestHandler<GetComplianceRatingsQuery, Result<IList<GetComplianceRatingsDto>>>
    {
        private readonly ISqlRepository<ComplianceRating, int> _complianceRatingsSqlRepository;
        private readonly IMapper _mapper;

        public GetComplianceRatingsQueryHandler(ISqlRepository<ComplianceRating, int> complianceRatingsSqlRepository, 
                                                IMapper mapper)
        {
            _complianceRatingsSqlRepository = complianceRatingsSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetComplianceRatingsDto>>> Handle(GetComplianceRatingsQuery request, CancellationToken cancellationToken)
        {
            var list = await _complianceRatingsSqlRepository.FindAsync(x => true );

            var ratings = list.ToList();
            if (!ratings.Any())
            {
                return Result.NotFound<IList<GetComplianceRatingsDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetComplianceRatingsDto> result = ratings.Select(s => _mapper.Map<GetComplianceRatingsDto>(s))
                .ToList();

            return Result.Ok(value: result);
        }
    }
}
