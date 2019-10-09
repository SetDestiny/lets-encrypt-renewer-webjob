using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace LetsEncryptRenewer.WebJob.Utils
{
    public class TelemetryLogger : ITelemetryLogger
    {
        private readonly TelemetryClient _telemetry;
        private Dictionary<string, string> Properties { get; set; }

        public TelemetryLogger(IConfiguration config, string configuration)
        {
            _telemetry = new TelemetryClient();
            var options = new ApplicationInsightsOptions();
            config.Bind(configuration, options);
            Properties = options.AdditionalLogProperties;
        }

        public void TrackTrace(string message)
        {
            _telemetry.TrackTrace(message, Properties);
        }

        public void TrackException(Exception ex)
        {
            _telemetry.TrackException(ex, Properties);
        }

        class ApplicationInsightsOptions
        {
            public Dictionary<string, string> AdditionalLogProperties
            {
                get; set;
            }
        }
    }
}
