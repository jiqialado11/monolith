using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffStatusesQuery
{
    public class GetStaffStatusesQuery : IRequest<Result<IList<GetStaffStatusesDto>>>
    {
    }
}
