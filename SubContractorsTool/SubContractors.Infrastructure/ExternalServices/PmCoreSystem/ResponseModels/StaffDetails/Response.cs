namespace SubContractors.Infrastructure.ExternalServices.PmCoreSystem.ResponseModels.StaffDetails
{
    public class Response
    {
        public Data Data { get; set; }
        
        public bool IsError { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}
