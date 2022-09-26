using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Compliance;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceRatingsQuery
{
    public class GetComplianceRatingsQuery:IRequest<Result<IList<GetComplianceRatingsDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(ComplianceRating).FullName;
        }
    }
}
