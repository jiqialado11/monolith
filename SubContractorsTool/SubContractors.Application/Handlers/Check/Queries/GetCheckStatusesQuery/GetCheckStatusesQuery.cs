using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Check.Queries.GetCheckStatusesQuery
{
    public class GetCheckStatusesQuery : IRequest<Result<IList<GetCheckStatusesDto>>>
    { }
}
