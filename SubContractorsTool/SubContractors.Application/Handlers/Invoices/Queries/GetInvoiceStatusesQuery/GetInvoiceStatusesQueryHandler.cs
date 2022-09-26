using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Invoice;

namespace SubContractors.Application.Handlers.Invoices.Queries.GetInvoiceStatusesQuery
{
    [RequestLogging]
    public class GetInvoiceStatusesQueryHandler : IRequestHandler<GetInvoiceStatusesQuery, Result<IList<GetInvoiceStatusesDto>>>
    {
        private readonly IMapper _mapper;

        public GetInvoiceStatusesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public  Task<Result<IList<GetInvoiceStatusesDto>>> Handle(GetInvoiceStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = Enum.GetValues(typeof(InvoiceStatus))
                .Cast<InvoiceStatus>()
                .ToList();
            if (!statuses.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetInvoiceStatusesDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetInvoiceStatusesDto> result = statuses.Select(x => _mapper.Map<GetInvoiceStatusesDto>(x))
                .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}
