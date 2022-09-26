using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Project.Commands.DeleteStaffProject
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteStaffProjectHandler : IRequestHandler<DeleteStaffProject, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStaffProjectHandler(ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, 
            ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, 
            IUnitOfWork unitOfWork)
        {
            _projectSqlRepository = projectSqlRepository;
            _staffSqlRepository = staffSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteStaffProject request, CancellationToken cancellationToken)
        {
            var staff = await _staffSqlRepository.GetAsync(request.StaffId.Value, new string[] {nameof(Domain.SubContractor.Staff.Staff.Projects) });
            if (staff == null)
            {
                return Result.NotFound<Unit>($"Staff wasn't found in database with provided identifier {request.StaffId.Value}");
            }

            var project = await _projectSqlRepository.GetAsync(request.ProjectId.Value, new string[] { });
            if (project == null)
            {
                return Result.NotFound<Unit>($"Project wasn't found in database with provided identifier {request.ProjectId}");
            }

            if (!staff.Projects.Contains(project))
            {
                return Result.Fail<Unit>(ResultType.BadRequest,$"Staff - {request.StaffId} doesn't contain Project with identifier - {request.ProjectId}");
            }

            staff.RemoveProject(project);

            await _staffSqlRepository.UpdateAsync(staff);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
