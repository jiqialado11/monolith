using SubContractors.Infrastructure.ExternalServices.MDPSystem.RequestModels.RegisterVendor;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.ContractorData;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LegalEntityData;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.LocationData;
using SubContractors.Infrastructure.ExternalServices.MDPSystem.ResponseModels.VendorData;
using System.Threading.Tasks;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem
{
    public interface IMdpSystemService
    {
        Task<PostVendorResponse> PostVendorAsync(Vendor vendorRequestModel);
        Task<VendorsResponse> GetSubcontractors();
        Task<LegalEntityResponse> GetLegalEntitiesAsync();
        Task<LocationsResponse> GetLocationsAsync();
        Task<ContractorResponse> GetContractorsAsync();
    }
}
