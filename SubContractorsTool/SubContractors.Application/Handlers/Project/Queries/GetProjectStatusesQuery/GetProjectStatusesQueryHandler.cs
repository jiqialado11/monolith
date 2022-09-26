using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Project;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectStatusesQuery
{
    [RequestLogging]
    public class GetProjectStatusesQueryHandler : IRequestHandler<GetProjectStatusesQuery, Result<IList<GetProjectStatusesDto>>>
    {
        private readonly IMapper _mapper;

        public GetProjectStatusesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<IList<GetProjectStatusesDto>>> Handle(GetProjectStatusesQuery request, CancellationToken cancellationToken)
        {
            var statuses = Enum.GetValues(typeof(ProjectStatus))
                .Cast<ProjectStatus>()
                .ToList();
            if (!statuses.Any())
            {
                return Task.FromResult(Result.NotFound<IList<GetProjectStatusesDto>>("Couldn't find entities with provided parameters"));
            }

            IList<GetProjectStatusesDto> result = statuses.Select(x => _mapper.Map<GetProjectStatusesDto>(x))
                .ToList();

            return Task.FromResult(Result.Ok(value: result));
        }
    }
}
