using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Check;

namespace SubContractors.Application.Handlers.Check.Queries.GetCheckStatusesQuery
{
    [RequestLogging]
    public class GetCheckStatusesQueryHandler : IRequestHandler<GetCheckStatusesQuery, Result<IList<GetCheckStatusesDto>>>
    {
        private readonly IMapper _mapper;

        public GetCheckStatusesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<IList<GetCheckStatusesDto>>> Handle(GetCheckStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = Enum.GetValues(typeof(CheckStatus))
                .Cast<CheckStatus>()
                .ToList();
            if (!statuses.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetCheckStatusesDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetCheckStatusesDto> result = statuses.Select(x => _mapper.Map<GetCheckStatusesDto>(x))
                .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}
