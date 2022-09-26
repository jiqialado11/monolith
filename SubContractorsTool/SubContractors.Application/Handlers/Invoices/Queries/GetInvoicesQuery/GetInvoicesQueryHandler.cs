using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoicesQuery
{
    [RequestLogging]
    [RequestValidation]
    [RequestCashing]
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, Result<IList<GetInvoicesDto>>>
    {
        private readonly IInvoiceSqlRepository _invoiceSqlRepository;
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly IMapper _mapper;
        public GetInvoicesQueryHandler(
            IInvoiceSqlRepository sqlRepository,
            ISqlRepository<SubContractor, int> subContractorSqlRepository, IMapper mapper)
        {
            _invoiceSqlRepository = sqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetInvoicesDto>>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            IList<GetInvoicesDto> result;
            IList<Domain.Invoice.Invoice> invoices;

            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId.Value);
                
            if (subContractor == null)
            {
                return Result.NotFound<IList<GetInvoicesDto>>($"subcontractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            invoices = (await _invoiceSqlRepository.FindAsync(x => x.SubContractor == subContractor))
                                                   .ToList();

            if (!invoices.Any())
            {
                return Result.NotFound<IList<GetInvoicesDto>>($"SubContractor with provided identifier {request.SubContractorId.Value} doesn't have invoices");
            }

            result = invoices.Select(s => _mapper.Map<GetInvoicesDto>(s)).ToList();

            return Result.Ok(value: result);
        }
    }
}