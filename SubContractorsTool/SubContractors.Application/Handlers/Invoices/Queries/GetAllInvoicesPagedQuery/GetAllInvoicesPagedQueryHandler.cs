using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Pagination;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetAllInvoicesPagedQuery
{
    [RequestLogging]
    [RequestCashing]
    public class GetAllInvoicesPagedQueryHandler : IRequestHandler<GetAllInvoicesPagedQuery, Result<PagedResult<GetAllInvoicesPagedDto>>>
    {
        private readonly IInvoiceSqlRepository _invoiceSqlRepository;
        public readonly IMapper _mapper;

        public GetAllInvoicesPagedQueryHandler(IInvoiceSqlRepository invoiceSqlRepository, IMapper mapper)
        {
            _invoiceSqlRepository = invoiceSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<GetAllInvoicesPagedDto>>> Handle(GetAllInvoicesPagedQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _invoiceSqlRepository.BrowseAsync(x => x.IsDeleted == false, request);

            if (pagedResult.IsEmpty)
            {
                return Result.NotFound<PagedResult<GetAllInvoicesPagedDto>>("There are no invoices according to the set filters");
            }

            var invoicesList = pagedResult.Items.Select(c => _mapper.Map<GetAllInvoicesPagedDto>(c));

            return Result.Ok(value: PagedResult<GetAllInvoicesPagedDto>.From(pagedResult, invoicesList));
        }
    }
}
