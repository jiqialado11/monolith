using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Common.Queries.GetLocationsQuery
{
    [RequestLogging]
    [RequestCashing]
    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, Result<IList<GetLocationsDto>>>
    {
        private readonly ISqlRepository<Domain.Common.Location, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetLocationsQueryHandler(ISqlRepository<Domain.Common.Location, int> sqlRepository,
                                        IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetLocationsDto>>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _sqlRepository.FindAsync(x => true);

            if (locations.Count() < 1)
            {
                return Result.NotFound<IList<GetLocationsDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetLocationsDto> result = locations.Select(s => _mapper.Map<GetLocationsDto>(s))                                                      
                                                     .ToList();

            return Result.Ok(value: result);
        }
    }
}