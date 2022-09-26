namespace SubContractors.Common.Hangfire
{
    public class HangfireOptions
    {
        public string ConnectionString { get; set; }
        public double CommandBatchMaxTimeout { get; set; }
        public double SlidingInvisibilityTimeout { get; set; }
        public bool DisableGlobalLocks { get; set; }
        public string Environment { get; set; }

        public string UiPath { get; set; }

        public bool AllowLocalConnection { get; set; }
    }
}
