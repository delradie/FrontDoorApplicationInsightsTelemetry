using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Web;

namespace Mercato.ApplicationInsights.Web.FrontDoor
{
    public class SyntheticBlankUserAgentTelemetryInitializer : ITelemetryInitializer
    {
        private const string SyntheticSourceNameKey = "Microsoft.ApplicationInsights.RequestTelemetry.SyntheticSource";
        private const string SyntheticSourceName = "Bot";

        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrWhiteSpace(telemetry.Context.Operation.SyntheticSource))
            {
                HttpContext CurrentContext = HttpContext.Current;

                if (CurrentContext?.Request != null)
                {
                    if (String.IsNullOrWhiteSpace(CurrentContext?.Request?.UserAgent))
                    {
                        telemetry.Context.Operation.SyntheticSource = SyntheticSourceName;
                        CurrentContext.Items.Add(SyntheticSourceNameKey, SyntheticSourceName);
                    }
                }
            }
        }
    }
}
