using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceQuery
{
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Result<GetInvoiceDto>>
    {
        private readonly IInvoiceSqlRepository _invoiceSqlRepository;
        private readonly IMapper _mapper;

        public GetInvoiceQueryHandler(IInvoiceSqlRepository invoiceRepository,
                                      IMapper mapper)
        {
            _invoiceSqlRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetInvoiceDto>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceSqlRepository.GetAsync(x => x.Id == request.Id.Value);

            if (invoice == null)
            {
                return Result.NotFound<GetInvoiceDto>($"Couldn't find invoice with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetInvoiceDto>(invoice);

            return Result.Ok(value: result);
        }
    }
}
