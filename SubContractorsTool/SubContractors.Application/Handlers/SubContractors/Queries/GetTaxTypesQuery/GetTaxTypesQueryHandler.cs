using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxTypesQuery
{
    [RequestLogging]
    public class GetTaxTypesQueryHandler : IRequestHandler<GetTaxTypesQuery, Result<IList<GetTaxTypeDto>>>
    {
        private readonly ISqlRepository<TaxType, int> _sqlRepository;
        private readonly IMapper _mapper;

        public GetTaxTypesQueryHandler(ISqlRepository<TaxType, int> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetTaxTypeDto>>> Handle(GetTaxTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true, Array.Empty<string>() );

            var taxTypes = list.ToList();
            if (!taxTypes.Any())
            {
                return Result.NotFound<IList<GetTaxTypeDto>>("Couldn't find entities with provided parameters");
            }

            IList<GetTaxTypeDto> result = taxTypes.Select(s => _mapper.Map<GetTaxTypeDto>(s))
                .ToList();

            return Result.Ok(value: result);
        }
    }
}
