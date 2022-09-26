using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.SubContractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Project.Queries.GetProjectListQuery
{
    [RequestValidation]
    [RequestLogging]
    public class GetProjectListQueryHandler : IRequestHandler<GetProjectListQuery, Result<IList<GetProjectListDto>>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly IMapper _mapper;

        public GetProjectListQueryHandler(
            ISqlRepository<SubContractor, int> subContractorSqlRepository, 
            ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, 
            IMapper mapper
            )
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetProjectListDto>>> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetAsync(request.SubContractorId.Value);
            if (subContractor == null)
            {
                return Result.NotFound<IList<GetProjectListDto>>($"Couldn't find subContractor with provided identifier {request.SubContractorId.Value}");
            }

            var projects = await _projectSqlRepository.FindAsync(x => x.SubContractors.Contains(subContractor),
                new[]
                {
                    nameof(Domain.Project.Project.ProjectGroup),
                    nameof(Domain.Project.Project.SubContractors),
                    nameof(Domain.Project.Project.ProjectManager),
                    nameof(Domain.Project.Project.InvoiceApprover),
                });

            if (projects == null || !projects.Any())
            {
                return Result.NotFound<IList<GetProjectListDto>>($"SubContractor with identifier {request.SubContractorId.Value} doesn't have projects");
            }

            IList<GetProjectListDto> result = projects.Select(x => _mapper.Map<GetProjectListDto>(x))
                .ToList();

            return Result.Ok(value: result);
        }
    }
}
