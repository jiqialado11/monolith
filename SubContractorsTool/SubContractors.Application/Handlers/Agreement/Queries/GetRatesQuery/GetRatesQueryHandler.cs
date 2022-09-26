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
using SubContractors.Domain.Agreement;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Agreement.Queries.GetRatesQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetRatesQueryHandler : IRequestHandler<GetRatesQuery, Result<IList<GetRatesDto>>>
    {
        private readonly IAddendaSqlRepository _sqlRepository;
        private readonly ISqlRepository<Rate, int> _rateSqlRepository;
        private readonly IMapper _mapper;

        public GetRatesQueryHandler(
            IAddendaSqlRepository sqlRepository, 
            ISqlRepository<Rate, int> rateSqlRepository, 
            IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _rateSqlRepository = rateSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetRatesDto>>> Handle(GetRatesQuery request, CancellationToken cancellationToken)
        {
            var addendum = await _sqlRepository.GetAsync(request.AddendumId.Value, Array.Empty<string>() );
            
            if (addendum == null)
            {
                return Result.NotFound<IList<GetRatesDto>>(
                    $"Couldn't find addendum with provided identifier {request.AddendumId.Value}");
            }

            var rates = await _rateSqlRepository.FindAsync(x => x.Addendum.Id == request.AddendumId.Value,
                new string[] { nameof(Rate.Addendum), nameof(Rate.Unit), nameof(Rate.Staff) });

            if (rates == null || !rates.Any())
            {
                return Result.NotFound<IList<GetRatesDto>>(
                    $"Addendum with identifier {request.AddendumId.Value} doesn't have rates");
            }

            IList<GetRatesDto> result = rates.Select(x => _mapper.Map<GetRatesDto>(x)).ToList();

            return Result.Ok(value: result);
        }
    }
}
