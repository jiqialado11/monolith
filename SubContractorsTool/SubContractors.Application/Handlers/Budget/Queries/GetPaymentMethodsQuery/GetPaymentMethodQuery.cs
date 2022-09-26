using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Budget.Queries.GetPaymentMethodsQuery
{
    public class GetPaymentMethodQuery : IRequest<Result<IList<GetPaymentMethodsDto>>>
    { }
}