using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Common;

namespace SubContractors.Application.Handlers.Common.Queries.GetLocationsQuery
{
    public class GetLocationsQuery : IRequest<Result<IList<GetLocationsDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(Location).FullName;
        }
    }

    
}