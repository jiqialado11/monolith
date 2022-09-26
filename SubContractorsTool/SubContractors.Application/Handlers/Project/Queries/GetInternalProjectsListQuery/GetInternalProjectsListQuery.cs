using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Queries.GetInternalProjectsListQuery
{
    public class GetInternalProjectsListQuery : IRequest<Result<IList<GetInternalProjectsListDto>>>
    { }
}