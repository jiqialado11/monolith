using SubContractors.Common.RestSharp;

namespace SubContractors.Infrastructure.ExternalServices.MDPSystem
{
    public class MdpSystemOptions : IMdpSystemOptions
    {
        public string CreateVendrPath { get; set; }
        public string LegalEntitiesPath { get; set; }
        public string LocationsPath { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        public string Domain { get; set; }
        public string AuthenticationType { get; set; }
        public string ContractorsPath { get; set; }
        public string VendorsPath { get; set; }
    }

    public interface IMdpSystemOptions : IRestSharpOptions
    {
        string CreateVendrPath { get; set; }
        string LegalEntitiesPath { get; set; }
        string LocationsPath { get; set; }
        string ContractorsPath { get; set; }
        string VendorsPath { get; set; }
    }
}
