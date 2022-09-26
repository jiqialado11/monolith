using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Infrastructure.Persistence.Repositories.Contracts;

namespace SubContractors.Application.Handlers.Project.Commands.DeleteProject
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class DeleteProjectHandler : IRequestHandler<DeleteProject, Result<Unit>>
    {
        private readonly ISubContractorSqlRepository _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectHandler(ISubContractorSqlRepository subContractorSqlRepository, ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, IUnitOfWork unitOfWork)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteProject request, CancellationToken cancellationToken)
        {
            var subContractor = await _subContractorSqlRepository.GetWithProjectRelatedEntitiesAsync(request.SubContractorId.Value);
            if (subContractor == null)
            {
                return Result.NotFound<Unit>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var project = await _projectSqlRepository.GetAsync(request.ProjectId.Value, new string[] { });
            if (project == null)
            {
                return Result.NotFound<Unit>($"Project wasn't found in database with provided identifier {request.ProjectId}");
            }

            foreach (var addendum in subContractor.Agreements.SelectMany(agreement => agreement.Addenda.Where(addendum => addendum.Projects.Contains(project))))
            {
                return Result.Fail<Unit>(ResultType.BadRequest,$"Couldn't remove project with identifier {request.ProjectId} from subcontractor because  it's linked with addendum {addendum.Id}");
            }

            foreach (var invoice in subContractor.Invoices.Where(invoice => invoice.Project.Id == project.Id))
            {
                return Result.Fail<Unit>(ResultType.BadRequest, $"Couldn't remove project with identifier {request.ProjectId} from subcontractor because  it's linked with invoice {invoice.Id}");
            }
            var successfullyRemoved = subContractor.RemoveProject(project);

            if (!successfullyRemoved)
            {
                return Result.NotFound<Unit>($"SubContractor with identifier {request.SubContractorId} doesn't have project with identifier {request.ProjectId}");
            }

            await _subContractorSqlRepository.UpdateAsync(subContractor);
            await _unitOfWork.SaveAsync();

            return Result.Ok();
        }
    }
}
