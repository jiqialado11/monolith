using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Staff.Queries.GetRateUnitsQuery
{
    [RequestLogging]
    [RequestCashing]
    public class GetRateUnitsQueryHandler : IRequestHandler<GetRateUnitsQuery, Result<IList<GetRateUnitDto>>>
    {
        private readonly ISqlRepository<RateUnit, int> _sqlRepository;

        public GetRateUnitsQueryHandler(ISqlRepository<RateUnit, int> sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public async Task<Result<IList<GetRateUnitDto>>> Handle(GetRateUnitsQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true);

            var rateUnits = list.ToList();
            if (!rateUnits.Any())
            {
                return Result.NotFound<IList<GetRateUnitDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetRateUnitDto> result = rateUnits.Select(s => new GetRateUnitDto
                                                     {
                                                         Id = s.Id,
                                                         Value = s.Value
                                                     })
                                                    .ToList();

            return Result.Ok(value: result);
        }
    }
}