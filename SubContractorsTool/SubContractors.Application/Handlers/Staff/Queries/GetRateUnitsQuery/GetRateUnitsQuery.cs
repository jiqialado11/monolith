using System.Collections.Generic;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Redis;
using SubContractors.Domain.Agreement;

namespace SubContractors.Application.Handlers.Staff.Queries.GetRateUnitsQuery
{
    public class GetRateUnitsQuery : IRequest<Result<IList<GetRateUnitDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(RateUnit).FullName;
        }
    }
}