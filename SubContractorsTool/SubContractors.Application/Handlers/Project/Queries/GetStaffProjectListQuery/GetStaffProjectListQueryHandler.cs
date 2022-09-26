using AutoMapper;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SubContractors.Application.Handlers.Project.Queries.GetStaffProjectListQuery
{
    [RequestValidation]
    [RequestLogging]
    public class GetStaffProjectListQueryHandler : IRequestHandler<GetStaffProjectListQuery, Result<IList<GetStaffProjectListDto>>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly IMapper _mapper;

        public GetStaffProjectListQueryHandler(
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, 
            ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, 
            IMapper mapper)
        {
            _staffSqlRepository = staffSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetStaffProjectListDto>>> Handle(GetStaffProjectListQuery request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(request.StaffId.Value);
            if (staff == null)
            {
                return Result.NotFound<IList<GetStaffProjectListDto>>($"Couldn't find staff with provided identifier {request.StaffId.Value}");
            }

            var projects = await _projectSqlRepository.FindAsync(x => x.Staffs.Contains(staff),
                new[]
                {
                    nameof(Domain.Project.Project.ProjectGroup),
                    nameof(Domain.Project.Project.SubContractors),
                    nameof(Domain.Project.Project.Staffs),
                    nameof(Domain.Project.Project.ProjectManager)
                });

            if (projects == null || !projects.Any())
            {
                return Result.NotFound<IList<GetStaffProjectListDto>>($"Staff with identifier {request.StaffId.Value} doesn't have projects");
            }

            IList<GetStaffProjectListDto> result = projects.Select(x => _mapper.Map<GetStaffProjectListDto>(x))
                .ToList();

            return Result.Ok(value: result);

        }
    }
}
