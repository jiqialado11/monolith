using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxTypesQuery
{
    public class GetTaxTypesQuery : IRequest<Result<IList<GetTaxTypeDto>>>
    {
    }
}
