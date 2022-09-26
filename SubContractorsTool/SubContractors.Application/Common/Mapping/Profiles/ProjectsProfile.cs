using AutoMapper;
using SubContractors.Application.Handlers.Project.Queries.GetInternalProjectsListQuery;
using SubContractors.Application.Handlers.Project.Queries.GetPmProjectQuery;
using SubContractors.Application.Handlers.Project.Queries.GetProjectListQuery;
using SubContractors.Application.Handlers.Project.Queries.GetProjectQuery;
using SubContractors.Application.Handlers.Project.Queries.GetProjectStatusesQuery;
using SubContractors.Application.Handlers.Project.Queries.GetStaffProjectListQuery;
using SubContractors.Application.Handlers.Project.Queries.GetSubContractorsProjectListByStaffQuery;
using SubContractors.Application.Handlers.Project.Queries.SearchPmProjectQuery;
using SubContractors.Common.Extensions;
using SubContractors.Domain.Project;

namespace SubContractors.Application.Common.Mapping.Profiles
{
    public class ProjectsProfile : Profile
    {
        public ProjectsProfile()
        {
            CreateMap<Project, GetProjectListDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.PmId))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.EstimatedFinishDate, o => o.MapFrom(source => source.EstimatedEndDate))
                .ForMember(dest => dest.FinishDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.StatusId, o => o.MapFrom(source => (int)source.Status))
                .ForMember(dest => dest.Status, o => o.MapFrom(source => source.Status.GetDescription()))
                .ForMember(dest => dest.ProjectGroupId, o => o.MapFrom(source => source.ProjectGroup.Id))
                .ForMember(dest => dest.ProjectGroupName, o => o.MapFrom(source => source.ProjectGroup.Name))
                .ForMember(dest => dest.ProjectGroupPmId, o => o.MapFrom(source => source.ProjectGroup.PmId))
                .ForMember(dest => dest.InvoiceApproverId, o => o.MapFrom(source => source.InvoiceApproverId  ))
                .ForMember(dest => dest.InvoiceApproverName,
                    o => o.MapFrom(source => source.InvoiceApprover != null ?
                        $"{source.InvoiceApprover.FirstName}  {source.InvoiceApprover.LastName}" : string.Empty))
                .ForMember(dest => dest.ProjectManagerId, o => o.MapFrom(source => source.ProjectManagerId))
                .ForMember(dest => dest.ProjectManager,
                    o => o.MapFrom(source => source.ProjectManager != null ?
                        $"{source.ProjectManager.FirstName}  {source.ProjectManager.LastName}" : string.Empty));


            CreateMap<Project, GetInternalProjectsListDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name));


            CreateMap<Project, GetStaffProjectListDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.FinishDate, o => o.MapFrom(source => source.EstimatedEndDate))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.StatusId, o => o.MapFrom(source => (int)source.Status))
                .ForMember(dest => dest.Status, o => o.MapFrom(source => source.Status.GetDescription()))
                .ForMember(dest => dest.ProjectGroupId, o => o.MapFrom(source => source.ProjectGroup.Id))
                .ForMember(dest => dest.ProjectGroupName, o => o.MapFrom(source => source.ProjectGroup.Name))
                .ForMember(dest => dest.ProjectManagerId, o => o.MapFrom(source => source.ProjectManager.Id))
                .ForMember(dest => dest.ProjectManagerName, o => o.MapFrom(source => $"{source.ProjectManager.FirstName} {source.ProjectManager.LastName}"));

            CreateMap<Project, GetSubContractorsProjectListByStaffDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.FinishDate, o => o.MapFrom(source => source.EstimatedEndDate))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.StatusId, o => o.MapFrom(source => (int)source.Status))
                .ForMember(dest => dest.Status, o => o.MapFrom(source => source.Status.GetDescription()))
                .ForMember(dest => dest.ProjectGroupId, o => o.MapFrom(source => source.ProjectGroup.Id))
                .ForMember(dest => dest.ProjectGroupName, o => o.MapFrom(source => source.ProjectGroup.Name))
                .ForMember(dest => dest.ProjectManagerId, o => o.MapFrom(source => source.ProjectManager.Id))
                .ForMember(dest => dest.ProjectManagerName, o => o.MapFrom(source => $"{source.ProjectManager.FirstName} {source.ProjectManager.LastName}"));

            CreateMap<Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.ProjectList.Project, SearchPmProjectDto>()
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.ProjectId))
                .ForMember(dest => dest.ProjectName, o => o.MapFrom(source => source.ProjectName));

            CreateMap<Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.ProjectDetails.Project,
                    GetPmProjectDto>()
                .ForMember(dest => dest.ProjectPmId, o => o.MapFrom(source => source.ProjectId))
                .ForMember(dest => dest.ProjectManagerId, o => o.MapFrom(source => source.ProjectLeaderId))
                .ForMember(dest => dest.EstimatedFinishDate, o => o.MapFrom(source => source.ProjectEstimatedEndDate))
                .ForMember(dest => dest.ProjectGroup, o => o.MapFrom(source => source.ProjectGroupName))
                .ForMember(dest => dest.ProjectGroupId, o => o.MapFrom(source => source.ProjectGroupId))
                .ForMember(dest => dest.ProjectManager, o => o.MapFrom(source => source.ProjectLeaderName))
                .ForMember(dest => dest.Status, o => o.Ignore())
                .ForMember(dest => dest.StatusId, o => o.Ignore())
                .ForMember(dest => dest.FinishDate, o => o.MapFrom(source => source.ProjectEndDate))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.ProjectStartDate));



            CreateMap<Project, GetProjectQueryDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => source.Id))
                .ForMember(dest => dest.PmId, o => o.MapFrom(source => source.PmId))
                .ForMember(dest => dest.ProjectName, o => o.MapFrom(source => source.Name))
                .ForMember(dest => dest.ProjectGroupId, o => o.MapFrom(source => source.ProjectGroup.Id))
                .ForMember(dest => dest.ProjectGroup, o => o.MapFrom(source => source.ProjectGroup.Name))
                .ForMember(dest => dest.ProjectGroupPmId, o => o.MapFrom(source => source.ProjectGroup.PmId))
                .ForMember(dest => dest.ProjectManagerId, o => o.MapFrom(source => source.ProjectManager.Id))
                .ForMember(dest => dest.ProjectManager,
                    o => o.MapFrom(source => $"{source.ProjectManager.FirstName} {source.ProjectManager.LastName}"))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(source => source.StartDate))
                .ForMember(dest => dest.EstimatedFinishDate, o => o.MapFrom(source => source.EstimatedEndDate))
                .ForMember(dest => dest.FinishDate, o => o.MapFrom(source => source.EndDate))
                .ForMember(dest => dest.ProjectStatusId, o => o.MapFrom(source => (int)source.Status))
                .ForMember(dest => dest.ProjectStatus, o => o.MapFrom(source => source.Status.GetDescription()));

            CreateMap<ProjectStatus, GetProjectStatusesDto>()
                .ForMember(dest => dest.Id, o => o.MapFrom(source => (int)source))
                .ForMember(dest => dest.Name, o => o.MapFrom(source => source.GetDescription()));

        }
    }
}
