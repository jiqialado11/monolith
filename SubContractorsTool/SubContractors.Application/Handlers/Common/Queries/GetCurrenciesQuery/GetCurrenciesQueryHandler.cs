using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Common;

namespace SubContractors.Application.Handlers.Common.Queries.GetCurrenciesQuery
{
    [RequestLogging]
    public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, Result<IList<GetCurrencyDto>>>
    {
        private readonly ISqlRepository<Currency, int> _sqlRepository;

        public GetCurrenciesQueryHandler(ISqlRepository<Currency, int> sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public async Task<Result<IList<GetCurrencyDto>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true);

            var currencies = list.ToList();
            if (!currencies.Any())
            {
                return Result.NotFound<IList<GetCurrencyDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetCurrencyDto> result = currencies.Select(s => new GetCurrencyDto
                                                      {
                                                          Id = s.Id,
                                                          Code = s.Code
                                                      })
                                                     .ToList();

            return Result.Ok(value: result);
        }
    }
}