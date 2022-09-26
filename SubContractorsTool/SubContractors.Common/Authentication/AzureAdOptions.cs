namespace SubContractors.Common.Authentication
{
    public class AzureAdOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
        public string Scoup { get; set; }
        public string CallbackPath { get; set; }
        public string Instance { get; set; }
    }
}
