using Serilog;

namespace SubContractors.Common.Logging
{
    public class FileLogOptions
    {
        public string Destination { get; set; }
        public string JsonDestination { get; set; }
        public RollingInterval RollingInterval { get; set; }
        public int RetainedFileCountLimit { get; set; }
        public bool IsJsonEnabled { get; set; }
        public bool Enabled { get; set; }
    }
}