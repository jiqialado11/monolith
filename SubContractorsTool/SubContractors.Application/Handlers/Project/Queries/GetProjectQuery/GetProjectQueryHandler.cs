using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectQuery
{
    [RequestLogging]
    [RequestValidation]
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, Result<GetProjectQueryDto>>
    {
        private readonly ISqlRepository<Domain.Project.Project, Guid> _sqlRepository;
        private readonly IMapper _mapper;

        public GetProjectQueryHandler(ISqlRepository<Domain.Project.Project, Guid> sqlRepository, IMapper mapper)
        {
            _sqlRepository = sqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetProjectQueryDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _sqlRepository.GetAsync(request.Id.Value, new[]
            {
                nameof(Domain.Project.Project.ProjectGroup),
                nameof(Domain.Project.Project.ProjectManager),
            });

            if (project == null)
            {
                return Result.NotFound<GetProjectQueryDto>($"Couldn't find entity with provided identifier {request.Id.Value}");
            }

            var result = _mapper.Map<GetProjectQueryDto>(project);

            return Result.Ok(value: result);
        }
    }
}
