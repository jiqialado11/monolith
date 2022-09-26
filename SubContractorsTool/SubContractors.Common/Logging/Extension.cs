using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Formatting.Compact;
using System;
using Microsoft.Extensions.Hosting;

namespace SubContractors.Common.Logging
{
    public static class Extension
    {
        public static LoggerConfiguration Configuration { get; private set; }

        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string applicationName = null)
        {
            return hostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var appOptions = context.Configuration.GetSection("app").Get<AppOptions>();
                var seqOptions = context.Configuration.GetSection("seq").Get<SeqOptions>();
                var fileLogOptions = context.Configuration.GetSection("fileLog").Get<FileLogOptions>();
                var serilogOptions = context.Configuration.GetSection("serilog").Get<SerilogOptions>();

                if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
                {
                    level = LogEventLevel.Information;
                }

                applicationName = string.IsNullOrWhiteSpace(applicationName) ? appOptions.Name : applicationName;
                loggerConfiguration.Enrich.FromLogContext()
                                   .MinimumLevel.Is(level)
                                   .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                                   .Enrich.WithProperty("ApplicationName", applicationName)
                                   .Enrich.WithMachineName()
                                   .Enrich.WithThreadId();

                Configure(loggerConfiguration, level, seqOptions, serilogOptions, fileLogOptions);
            });
        }

        public static ReloadableLogger PrepareLogger(IConfigurationRoot configuration, string applicationName = null)
        {
            var appOptions = configuration.GetSection("app").Get<AppOptions>();
            var seqOptions = configuration.GetSection("seq").Get<SeqOptions>();
            var fileLogOptions = configuration.GetSection("fileLog").Get<FileLogOptions>();
            var serilogOptions = configuration.GetSection("serilog").Get<SerilogOptions>();

            if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
            {
                level = LogEventLevel.Information;
            }

            applicationName = string.IsNullOrWhiteSpace(applicationName) ? appOptions.Name : applicationName;
            Configuration = new LoggerConfiguration().ReadFrom.Configuration(configuration, "serilog")
                                                     .Enrich.FromLogContext()
                                                     .MinimumLevel.Is(level)
                                                     .Enrich.WithProperty("ApplicationName", applicationName)
                                                     .Enrich.WithMachineName()
                                                     .Enrich.WithThreadId();

            Configure(Configuration, level, seqOptions, serilogOptions, fileLogOptions);

            return Configuration.CreateBootstrapLogger();
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, LogEventLevel level, SeqOptions seqOptions, SerilogOptions serilogOptions, FileLogOptions fileLogOptions)
        {
            if (seqOptions.Enabled)
            {
                loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
            }

            if (fileLogOptions.Enabled)
            {
                if (!fileLogOptions.IsJsonEnabled)
                {
                    loggerConfiguration.WriteTo.File(fileLogOptions.Destination, retainedFileCountLimit: fileLogOptions.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                }
                else
                {
                    loggerConfiguration.WriteTo.File(new CompactJsonFormatter(), fileLogOptions.JsonDestination, retainedFileCountLimit: fileLogOptions.RetainedFileCountLimit, rollingInterval: RollingInterval.Day);
                }
            }

            if (serilogOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo.Console();
            }
        }
    }
}