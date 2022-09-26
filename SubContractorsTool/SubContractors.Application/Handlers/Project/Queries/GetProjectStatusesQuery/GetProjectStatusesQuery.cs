using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectStatusesQuery
{
    public class GetProjectStatusesQuery : IRequest<Result<IList<GetProjectStatusesDto>>>
    {
    }
}
