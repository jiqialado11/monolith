using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Budget.Queries.GetPaymentTermsQuery
{
    public class GetPaymentTermsQuery : IRequest<Result<IList<GetPaymentTermsDto>>>
    { }
}