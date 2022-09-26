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

namespace SubContractors.Application.Handlers.Project.Queries.GetInternalProjectsListQuery
{
    [RequestLogging]
    public class GetInternalProjectsListQueryHandler : IRequestHandler<GetInternalProjectsListQuery, Result<IList<GetInternalProjectsListDto>>>
    {
        private readonly ISqlRepository<Domain.Project.Project, Guid> _sqlRepository;
        private readonly IMapper _mapper;

        public GetInternalProjectsListQueryHandler(ISqlRepository<Domain.Project.Project, Guid> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetInternalProjectsListDto>>> Handle(GetInternalProjectsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _sqlRepository.FindAsync(x => true);

            var projects = list.ToList();
            if (!projects.Any())
            {
                return Result.NotFound<IList<GetInternalProjectsListDto>>("Couldn't find entities");
            }

            var result = projects.Select(s => _mapper.Map<GetInternalProjectsListDto>(s)).ToList() as IList<GetInternalProjectsListDto>;

            return Result.Ok(value: result);
        }
    }
}