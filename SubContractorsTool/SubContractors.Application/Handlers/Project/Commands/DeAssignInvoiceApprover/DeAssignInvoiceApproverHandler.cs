using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;

namespace SubContractors.Application.Handlers.Project.Commands.DeAssignInvoiceApprover
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeAssignInvoiceApproverHandler : IRequestHandler<DeAssignInvoiceApprover, Result<Unit>>
    {
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeAssignInvoiceApproverHandler(ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, IUnitOfWork unitOfWork)
        {
            _projectSqlRepository = projectSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeAssignInvoiceApprover request, CancellationToken cancellationToken)
        {
            var project = await _projectSqlRepository.GetAsync(x => x.Id == request.ProjectId, new []{nameof(Domain.Project.Project.InvoiceApprover)});
            if (project == null)
            {
                return Result.NotFound<Unit>($"Project wasn't found in database with provided identifier {request.ProjectId}");
            }


            if (project.InvoiceApprover == null)
            {
                return Result.NotFound<Unit>($"Project doesn't has assigned invoice approver");
            }
            project.DeAssignInvoiceApprover();
            await _projectSqlRepository.UpdateAsync(project);
            await _unitOfWork.SaveAsync();

            return Result.Accepted();
        }
    }
}
