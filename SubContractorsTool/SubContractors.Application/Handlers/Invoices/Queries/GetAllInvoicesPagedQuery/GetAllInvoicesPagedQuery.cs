using FluentValidation;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Common.Redis;
using SubContractors.Domain.Invoice;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetAllInvoicesPagedQuery
{
    public class GetAllInvoicesPagedQuery : PagedQueryBase, IRequest<Result<PagedResult<GetAllInvoicesPagedDto>>>, ICacheableRequest
    {
        public string GetDomainIdentifier()
        {
            return typeof(Invoice).FullName;
        }
    }

    public class GetAllInvoicesQueryValidation : AbstractValidator<GetAllInvoicesPagedQuery>
    {
        public GetAllInvoicesQueryValidation()
        { }
    }
}
