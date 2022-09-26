using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsTypeQuery
{
    public class GetSubContractorsTypeQuery : IRequest<Result<IList<GetSubContractorTypesDto>>>
    { }
}