using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using SubContractors.Domain.SubContractor.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxesQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetTaxesQueryHandler : IRequestHandler<GetTaxesQuery, Result<IList<GetTaxesDto>>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Tax, int> _taxSqlRepository;
        private readonly IMapper _mapper;

        public GetTaxesQueryHandler(ISqlRepository<SubContractor, int> subContractorSqlRepository,
            ISqlRepository<Tax, int> taxSqlRepository, IMapper mapper)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _taxSqlRepository = taxSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetTaxesDto>>> Handle(GetTaxesQuery request, CancellationToken cancellationToken)
        {
            var subContractor =
                await _subContractorSqlRepository.GetAsync(request.SubContractorId.Value, Array.Empty<string>() );
            if (subContractor == null)
            {
                return Result.NotFound<IList<GetTaxesDto>>(
                    $"Couldn't find subContractor with provided identifier {request.SubContractorId.Value}");
            }

            var taxes = await _taxSqlRepository.FindAsync(x => x.SubContractor.Id == request.SubContractorId.Value,
                new string[] { nameof(Tax.SubContractor), nameof(Tax.TaxType) });

            if (taxes == null || !taxes.Any())
            {
                return Result.NotFound<IList<GetTaxesDto>>(
                    $"SubContractor with identifier {request.SubContractorId.Value} doesn't have taxes");
            }

            IList<GetTaxesDto> result = taxes.Select(x => _mapper.Map<GetTaxesDto>(x)).ToList();

            return Result.Ok(value: result);
        }
    }
}
