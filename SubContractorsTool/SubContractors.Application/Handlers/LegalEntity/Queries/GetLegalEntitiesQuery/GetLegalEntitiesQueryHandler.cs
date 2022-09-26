using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.LegalEntity.Queries.GetLegalEntitiesQuery
{
    [RequestLogging]
    [RequestCashing]
    public class GetLegalEntitiesQueryHandler : IRequestHandler<GetLegalEntitiesQuery, Result<IList<GetLegalEntitiesDto>>>
    {
        private readonly ISqlRepository<Domain.SubContractor.LegalEntity, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetLegalEntitiesQueryHandler(ISqlRepository<Domain.SubContractor.LegalEntity, int> sqlRepository,
                                            IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetLegalEntitiesDto>>> Handle(GetLegalEntitiesQuery request, CancellationToken cancellationToken)
        {
            var legalEntities = await _sqlRepository.FindAsync(x => x.IsActive);

            if (legalEntities.Count() < 1)
            {
                return Result.NotFound<IList<GetLegalEntitiesDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetLegalEntitiesDto> result = legalEntities.Select(s => _mapper.Map<GetLegalEntitiesDto>(s))
                                                             .ToList();

            return Result.Ok(value: result);
        }
    }
}