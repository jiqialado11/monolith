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
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Project.Queries.GetSubContractorsProjectListByStaffQuery
{
    [RequestValidation]
    [RequestLogging]
    public class GetSubContractorsProjectListByStaffQueryHandler : IRequestHandler<GetSubContractorsProjectListByStaffQuery, Result<IList<GetSubContractorsProjectListByStaffDto>>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IProjectSqlRepository _projectSqlRepository;
        private readonly ISubContractorSqlRepository _subContractorSqlRepository;
        private readonly IMapper _mapper;

        public GetSubContractorsProjectListByStaffQueryHandler(
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository,
            IProjectSqlRepository projectSqlRepository, 
            ISubContractorSqlRepository subContractorSqlRepository, 
            IMapper mapper)
        {
            _staffSqlRepository = staffSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _subContractorSqlRepository = subContractorSqlRepository;
            _mapper = mapper;
        }

        public async Task<Result<IList<GetSubContractorsProjectListByStaffDto>>> Handle(GetSubContractorsProjectListByStaffQuery request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(request.StaffId.Value, new[] { nameof(Domain.SubContractor.Staff.Staff.SubContractors) });
            if (staff == null)
            {
                return Result.NotFound<IList<GetSubContractorsProjectListByStaffDto>>($"Couldn't find staff with provided identifier {request.StaffId.Value}");
            }

            if (staff.SubContractors == null || !staff.SubContractors.Any())
            {
                return Result.NotFound<IList<GetSubContractorsProjectListByStaffDto>>($"Staff with identifier - {request.StaffId} doesn't has subcontractors");
            }

            var subContractorIdentifiers = staff.SubContractors.Select(x => x.Id);
            var projects = await _projectSqlRepository.GetProjectsBySubContractorsIdentifiers(subContractorIdentifiers.ToList());

            if (projects == null || !projects.Any())
            {
                return Result.NotFound<IList<GetSubContractorsProjectListByStaffDto>>($"Staff with identifier {request.StaffId.Value} doesn't have projects related with subcontractors");
            }

            IList<GetSubContractorsProjectListByStaffDto> result = projects.Select(x => _mapper.Map<GetSubContractorsProjectListByStaffDto>(x))
                .ToList();

            return Result.Ok(value: result);
        }
    }
}
