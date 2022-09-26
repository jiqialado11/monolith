using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.SubContractors.Queries.GetSubContractorsStatusesQuery
{
    [RequestLogging]
    public class GetSubContractorsStatusesQueryHandler : IRequestHandler<GetSubContractorsStatusesQuery, Result<IList<GetSubContractorsStatusesDto>>>
    {
        private readonly IMapper _mapper;

        public GetSubContractorsStatusesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<IList<GetSubContractorsStatusesDto>>> Handle(GetSubContractorsStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = Enum.GetValues(typeof(SubContractorStatus))
                               .Cast<SubContractorStatus>()
                               .ToList();
            if (!statuses.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetSubContractorsStatusesDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetSubContractorsStatusesDto> result = statuses.Select(x => _mapper.Map<GetSubContractorsStatusesDto>(x))
                                                                 .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}