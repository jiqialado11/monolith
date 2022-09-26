namespace SubContractors.Common.Redis
{
    public class CachingOptions : ICachingOptions
    {
        public string ConnectionString { get; set; }
        public string Instance { get; set; }
        public int AbsoluteExpirationRelativeToNow { get; set; }
        public int SlidingExpiration { get; set; }
        public bool AbortOnConnectFail { get; set; }
    }

    public interface ICachingOptions
    {
        public string ConnectionString { get; set; }
        public string Instance { get; set; }
        public int AbsoluteExpirationRelativeToNow { get; set; }
        public int SlidingExpiration { get; set; }
        public bool AbortOnConnectFail { get; set; }
    }
}
