using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetMarketsQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetMarketsQueryHandler : IRequestHandler<GetMarketsQuery, Result<IList<GetMarketsDto>>>
    {
        private readonly ISqlRepository<Market, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetMarketsQueryHandler(ISqlRepository<Market, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetMarketsDto>>> Handle(GetMarketsQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true);

            if (!list.Any())
            {
                return Result.NotFound<IList<GetMarketsDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetMarketsDto> result = list.Select(x => _mapper.Map<GetMarketsDto>(x))
                                              .ToList();

            return Result.Ok(value: result);
        }
    }
}