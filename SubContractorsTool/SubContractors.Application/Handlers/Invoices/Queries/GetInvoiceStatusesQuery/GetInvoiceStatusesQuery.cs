using System.Collections.Generic;
using MediatR;
using SubContractors.Common;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceStatusesQuery
{
    public class GetInvoiceStatusesQuery : IRequest<Result<IList<GetInvoiceStatusesDto>>>
    {
    }
}
