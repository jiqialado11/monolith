using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Common.Queries.GetCurrenciesQuery
{
    public class GetCurrenciesQuery : IRequest<Result<IList<GetCurrencyDto>>>
    { }
}