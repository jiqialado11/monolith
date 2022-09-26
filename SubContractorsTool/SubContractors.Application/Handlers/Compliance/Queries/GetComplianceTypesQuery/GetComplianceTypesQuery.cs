using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Compliance.Queries.GetComplianceTypesQuery
{
    public class GetComplianceTypesQuery:IRequest<Result<IList<GetComplianceTypeDto>>>
    { }
}
