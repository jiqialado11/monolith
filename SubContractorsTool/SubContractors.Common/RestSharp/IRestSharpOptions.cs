namespace SubContractors.Common.RestSharp
{
    public interface IRestSharpOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        public string Domain { get; set; }
        public string AuthenticationType { get; set; }
    }
}
