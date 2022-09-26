using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetOfficesQuery
{
    public class GetOfficesQuery : IRequest<Result<IList<GetOfficesDto>>>, ICacheableRequest
    {
        public int? OfficeTypeId { get; set; }

        public string GetDomainIdentifier()
        {
            return typeof(Office).FullName;
        }
    }
}