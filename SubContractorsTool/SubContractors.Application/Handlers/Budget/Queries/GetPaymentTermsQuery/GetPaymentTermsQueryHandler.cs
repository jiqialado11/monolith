using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Budget;

namespace SubContractors.Application.Handlers.Budget.Queries.GetPaymentTermsQuery
{
    [RequestLogging]
    public class GetPaymentTermsQueryHandler : IRequestHandler<GetPaymentTermsQuery, Result<IList<GetPaymentTermsDto>>>
    {
        private readonly ISqlRepository<PaymentTerm, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetPaymentTermsQueryHandler(ISqlRepository<PaymentTerm, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetPaymentTermsDto>>> Handle(GetPaymentTermsQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true, new string[] {});

            var paymentTerms = list.ToList();
            if (!paymentTerms.Any())
            {
                return Result.NotFound<IList<GetPaymentTermsDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetPaymentTermsDto> result = paymentTerms.Select(s => _mapper.Map<GetPaymentTermsDto>(s))
                                                           .ToList();

            return Result.Ok(value: result);
        }
    }
}