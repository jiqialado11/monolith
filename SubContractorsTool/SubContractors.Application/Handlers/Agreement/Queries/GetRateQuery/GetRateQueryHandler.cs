using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Agreement;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetRateQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetRateQueryHandler : IRequestHandler<GetRateQuery, Result<GetRateDto>>
    {
        private readonly ISqlRepository<Rate, int> _rateSqlRepository;
        private readonly IMapper _mapper;

        public GetRateQueryHandler(ISqlRepository<Rate, int> rateSqlRepository, IMapper mapper)
        {
            _rateSqlRepository = rateSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetRateDto>> Handle(GetRateQuery request, CancellationToken cancellationToken)
        {
            var rate = await _rateSqlRepository.GetAsync(request.RateId.Value,
                new string[] { nameof(Rate.Addendum), nameof(Rate.Unit), nameof(Rate.Staff) });
            if (rate == null)
            {
                return Result.NotFound<GetRateDto>($"Couldn't find rate with provided identifier {request.RateId.Value}");
            }

            return Result.Ok(value: _mapper.Map<GetRateDto>(rate));
        }
    }
}
