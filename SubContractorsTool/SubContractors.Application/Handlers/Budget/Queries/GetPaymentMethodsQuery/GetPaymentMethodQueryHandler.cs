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

namespace SubContractors.Application.Handlers.Budget.Queries.GetPaymentMethodsQuery
{
    [RequestLogging]
    public class GetPaymentMethodQueryHandler : IRequestHandler<GetPaymentMethodQuery, Result<IList<GetPaymentMethodsDto>>>
    {
        private readonly ISqlRepository<PaymentMethod, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetPaymentMethodQueryHandler(ISqlRepository<PaymentMethod, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetPaymentMethodsDto>>> Handle(GetPaymentMethodQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true, new string[] {});

            var paymentMethods = list.ToList();
            if (!paymentMethods.Any())
            {
                return Result.NotFound<IList<GetPaymentMethodsDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetPaymentMethodsDto> result = paymentMethods.Select(s => _mapper.Map<GetPaymentMethodsDto>(s))
                                                               .ToList();

            return Result.Ok(value: result);
        }
    }
}