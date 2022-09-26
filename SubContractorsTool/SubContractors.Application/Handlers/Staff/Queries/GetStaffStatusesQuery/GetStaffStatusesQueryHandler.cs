using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor.Staff;

namespace SubContractors.Application.Handlers.Staff.Queries.GetStaffStatusesQuery
{
    [RequestLogging]
    public class GetStaffStatusesQueryHandler : IRequestHandler<GetStaffStatusesQuery, Result<IList<GetStaffStatusesDto>>>
    {
        private readonly IMapper _mapper;

        public GetStaffStatusesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<IList<GetStaffStatusesDto>>> Handle(GetStaffStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = Enum.GetValues(typeof(StaffStatus))
                .Cast<StaffStatus>()
                .ToList();
            if (!statuses.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetStaffStatusesDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetStaffStatusesDto> result = statuses.Select(x => _mapper.Map<GetStaffStatusesDto>(x))
                .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}
