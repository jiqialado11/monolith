using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Staff.Commands.AssignProject
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class AssignProjectHandler : IRequestHandler<AssignProject, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectsSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignProjectHandler(ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, ISqlRepository<Domain.Project.Project, Guid> projectsSqlRepository, IUnitOfWork unitOfWork)
        {
            _staffSqlRepository = staffSqlRepository;
            _projectsSqlRepository = projectsSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AssignProject request, CancellationToken cancellationToken)
        {
            var project = await _projectsSqlRepository.GetAsync(x => x.Id == request.ProjectId, new []{nameof(Domain.Project.Project.Staffs)});
            if (project == null)
            {
                return Result.NotFound<Unit>($"Project wasn't found in database with provided identifier {request.ProjectId}");
            }

            var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.StaffId);
            if (staff == null)
            {
                return Result.NotFound<Unit>($"Staff wasn't found in database with provided identifier {request.StaffId}");
            }

            var successfullyAssignToProject =  staff.AssignToProject(project);

            if (!successfullyAssignToProject)
            {
                return Result.Fail<Unit>(ResultType.BadRequest,$"Project with identifier - {request.ProjectId} already contains staff - {request.StaffId}");
            }
            await _staffSqlRepository.UpdateAsync(staff);
            await _unitOfWork.SaveAsync();

            return Result.Success(ResultType.Accepted);
        }
    }
}
