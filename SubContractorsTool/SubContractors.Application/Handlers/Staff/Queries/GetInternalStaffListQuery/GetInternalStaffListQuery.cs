using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;

namespace SubContractors.Application.Handlers.Staff.Queries.GetInternalStaffListQuery
{
    public class GetInternalStaffListQuery : IRequest<Result<IList<GetInternalStaffListDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(Domain.SubContractor.Staff.Staff).FullName;
        }
    }
}
