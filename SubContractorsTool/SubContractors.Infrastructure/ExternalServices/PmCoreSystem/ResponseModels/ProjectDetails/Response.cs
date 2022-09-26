namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.ProjectDetails
{
    public class Response
    {
        public Project Project { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }
    }
}
