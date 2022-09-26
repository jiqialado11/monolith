using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Staff.Queries.SearchPmStaffQuery
{
    public class SearchPmStaffQuery: IRequest<Result<IList<SearchPmStaffDto>>>
    {
    }
}
