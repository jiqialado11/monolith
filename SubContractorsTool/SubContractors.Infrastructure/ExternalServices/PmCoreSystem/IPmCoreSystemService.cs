using System.Threading.Tasks;
using SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffList;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem
{
    public interface IPmCoreSystemService
    {
        Task<Response> GetStaffListAsync();

        Task<ResponseModels.DetailStaffList.Response> GetStaffListPerLastDaysAsync(int days);

        Task<ResponseModels.StaffDetails.Response> GetStaffDetailsAsync(int staffId);

        Task<ResponseModels.ProjectList.Response> GetProjectListAsync();

        Task<ResponseModels.ProjectDetails.Response> GetProjectDetailsAsync(int projectId);
    }
}
