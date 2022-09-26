using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsQuery
{
    public class GetSubContractorsQuery : IRequest<Result<IList<GetSubContractorsDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(SubContractor).FullName;
        }
    }
}