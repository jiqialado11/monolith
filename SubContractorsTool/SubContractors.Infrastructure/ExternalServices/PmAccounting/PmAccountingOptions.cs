using SubContractors.Common.RestSharp;

namespace SubContractors.Infrastructure.ExternalServices.PmAccounting
{
    public class PmAccountingOptions : IPmAccountingOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        public string Domain { get; set; }
        public string AuthenticationType { get; set; }
        public string MilestonesPath { get; set; }
    }

    public interface IPmAccountingOptions : IRestSharpOptions
    {
        public string MilestonesPath { get; set; }
    }
}
