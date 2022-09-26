using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;

namespace SubContractors.Application.Handlers.LegalEntity.Queries.GetLegalEntitiesQuery
{
    public class GetLegalEntitiesQuery : IRequest<Result<IList<GetLegalEntitiesDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(Domain.SubContractor.LegalEntity).FullName;
        }
    }
}