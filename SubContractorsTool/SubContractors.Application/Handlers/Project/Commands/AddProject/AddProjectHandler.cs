using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SubContractors.Application.Handlers.Staff.Commands.RetrieveAndCreateStaff;
using SubContractors.Common;
using SubContractors.Common.EfCore.Contracts;
using SubContractors.Common.Mediator;
using SubContractors.Common.Mediator.Attributes;
using SubContractors.Domain.Project;
using SubContractors.Domain.SubContractor;

namespace SubContractors.Application.Handlers.Project.Commands.AddProject
{
    [RequestLogging]
    [RequestValidation]
    [RequestTransaction]
    public class AddProjectHandler : IRequestHandler<AddProject, Result<Guid>>
    {
        private readonly ISqlRepository<SubContractor, int> _subContractorSqlRepository;
        private readonly ISqlRepository<Domain.Project.Project, Guid> _projectSqlRepository;
        private readonly ISqlRepository<Domain.SubContractor.Staff.Staff, int> _staffSqlRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;
        private readonly ISqlRepository<ProjectGroup, int> _projectGroupSqlRepository;

        public AddProjectHandler(
            ISqlRepository<SubContractor, int> subContractorSqlRepository, 
            ISqlRepository<Domain.Project.Project, Guid> projectSqlRepository, 
            IUnitOfWork unitOfWork, ISqlRepository<Domain.SubContractor.Staff.Staff, int> staffSqlRepository, 
            IDispatcher dispatcher, ISqlRepository<ProjectGroup, int> projectGroupSqlRepository)
        {
            _subContractorSqlRepository = subContractorSqlRepository;
            _projectSqlRepository = projectSqlRepository;
            _unitOfWork = unitOfWork;
            _staffSqlRepository = staffSqlRepository;
            _dispatcher = dispatcher;
            _projectGroupSqlRepository = projectGroupSqlRepository;
        }

        public async Task<Result<Guid>> Handle(AddProject request, CancellationToken cancellationToken)
        {
            var projectExist = true;
            var subContractor = await _subContractorSqlRepository.GetAsync(x => x.Id == request.SubContractorId);
            if (subContractor == null)
            {
                return Result.NotFound<Guid>($"SubContractor wasn't found in database with provided identifier {request.SubContractorId}");
            }

            var project = await _projectSqlRepository.GetAsync(x => x.PmId == request.PmId, new string[]
            {
                nameof(Domain.Project.Project.ProjectManager),
                nameof(Domain.Project.Project.ProjectGroup),
                nameof(Domain.Project.Project.SubContractors)
            });

            if (project == null)
            {
                project = new Domain.Project.Project();
                project.Create(request.PmId.Value, request.ProjectName, request.StartDate, request.FinishDate, request.EstimatedFinishDate);
                project.AssignStatus(request.ProjectStatusId);
                projectExist = false;
            }
        

            if (request.ProjectManagerId.HasValue)
            {
                var projectManager = await _staffSqlRepository.GetAsync(x => x.PmId == request.ProjectManagerId);
                if (projectManager == null)
                {
                    var createStaffRequest = new RetrieveAndCreateStaff
                    {
                        PmId = request.ProjectManagerId.Value,
                        SubContractorId = request.SubContractorId.Value,
                    };

                    var createStaffResult = await _dispatcher.RequestAsync(createStaffRequest);

                    if (!createStaffResult.IsSuccess)
                    {
                        return Result.Fail<Guid>(ResultType.BadRequest, $"Wasn't able to retrieve and create staff from PM system. StaffId - {request.ProjectManagerId}");
                    }

                    projectManager = await _staffSqlRepository.GetAsync(x => x.Id == createStaffResult.Data);
                }
                
                project.AssignProjectManager(projectManager);
            }


            if (request.ProjectGroupId.HasValue)
            {
                var projectGroup = await _projectGroupSqlRepository.GetAsync(x => x.PmId == request.ProjectGroupId);

                if (projectGroup == null)
                {
                    projectGroup = new ProjectGroup();
                    projectGroup.Create(request.ProjectGroupId.Value, request.ProjectGroup);
                }
                project.AssignProjectGroup(projectGroup);
            }
         

            var assignmentResult = subContractor.AssignProject(project);
            if (!assignmentResult)
            {
                return Result.Fail<Guid>(ResultType.BadRequest, $"Project with Id - {request.PmId} has already been assigned to subContractor with Id - {request.SubContractorId}");
            }
            if (projectExist)
            {
                await _projectSqlRepository.UpdateAsync(project);
            }
            else
            {
                await _projectSqlRepository.AddAsync(project);
            }

            await _unitOfWork.SaveAsync();

            return await Task.FromResult(Result.Success(ResultType.Accepted, data: project.Id));
        }
    }
}