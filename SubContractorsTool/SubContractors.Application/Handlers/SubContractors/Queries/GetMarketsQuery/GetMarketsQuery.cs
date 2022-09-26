using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetMarketsQuery
{
    public class GetMarketsQuery : IRequest<Result<IList<GetMarketsDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(Market).FullName;
        }
    }
}