using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsStatusesQuery
{
    public class GetSubContractorsStatusesQuery : IRequest<Result<IList<GetSubContractorsStatusesDto>>>
    { }
}