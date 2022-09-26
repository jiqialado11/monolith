using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor.Tax;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetTaxQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetTaxQueryHandler : IRequestHandler<GetTaxQuery, Result<GetTaxDto>>
    {
        private readonly ISqlRepository<Tax, int> _taxSqlRepository;
        private readonly IMapper _mapper
            ;

        public GetTaxQueryHandler(
            ISqlRepository<Tax, int> taxSqlRepository, 
            IMapper mapper
            )
        {
            _taxSqlRepository = taxSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetTaxDto>> Handle(GetTaxQuery request, CancellationToken cancellationToken)
        {
            var tax = await _taxSqlRepository.GetAsync(request.Id.Value, new string[]{nameof(Tax.SubContractor), nameof(Tax.TaxType)});
            if (tax == null)
            {
                return Result.NotFound<GetTaxDto>($"Couldn't find tax with provided identifier {request.Id.Value}");
            }

            return Result.Ok(value: _mapper.Map<GetTaxDto>(tax));

        }
    }
}
