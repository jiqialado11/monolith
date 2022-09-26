using SubContractors.Common.RestSharp;

namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem
{
    public class PmCoreSystemOptions : IPmCoreSystemOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        public string Domain { get; set; }
        public string AuthenticationType { get; set; }
        public string NewlyAddedStaffList { get; set; }
        public string StaffListPath { get; set; }
        public string StaffDetailsPath { get; set; }
        public string ProjectListPath { get; set; }
        public string ProjectDetailsPath { get; set; }
    }

    public interface IPmCoreSystemOptions : IRestSharpOptions
    {
        public string NewlyAddedStaffList { get; set; }
        public string StaffListPath { get; set; }
        public string StaffDetailsPath { get; set; }
        public string ProjectListPath { get; set; }
        public string ProjectDetailsPath { get; set; }
    }
}
