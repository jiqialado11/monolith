using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Project.Commands.AssignInvoiceApprover
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class AssignInvoiceApproverHandler : IRequestHandler<AssignInvoiceApprover, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignInvoiceApproverHandler(ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, IUnitOfWork unitOfWork)
        {
            _staffSqlRepository = staffSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AssignInvoiceApprover request, CancellationToken cancellationToken)
        {
            var project = await _projectSqlRepository.GetAsync(x => x.Id == request.ProjectId);
            if (project == null)
            {
                return Result.NotFound<Unit>($"Project wasn't found in database with provided identifier {request.ProjectId}");
            }


            var staff = await _staffSqlRepository.GetAsync(x => x.Id == request.StaffId);
            if (staff == null)
            {
                return Result.NotFound<Unit>($"Staff wasn't found in database with provided identifier {request.StaffId}");
            }

            project.AssignInvoiceApprover(staff);
            await _projectSqlRepository.UpdateAsync(project);
            await _unitOfWork.SaveAsync();

            return Result.Accepted();
        }
    }
}
