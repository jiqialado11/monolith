using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Project.Queries.SearchPmProjectQuery
{
    public class SearchPmProjectQuery : IRequest<Result<IList<SearchPmProjectDto>>>
    {
    }
}
