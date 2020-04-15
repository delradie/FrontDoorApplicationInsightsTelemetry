using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercato.ApplicationInsights.Web.FrontDoor
{
    public class HttpHeaderTelemetryInitializer : ITelemetryInitializer
    {
        public List<String> RequestHeaderWhitelist { get; set; }
        public List<String> ResponseHeaderWhitelist { get; set; }

        public HttpHeaderTelemetryInitializer()
        {
            this.RequestHeaderWhitelist = new List<String>();
            this.ResponseHeaderWhitelist = new List<String>();
        }

        public HttpHeaderTelemetryInitializer(IEnumerable<String> requestHeaderWhitelist, IEnumerable<String> responseHeaderWhitelist)
        {
            this.RequestHeaderWhitelist = new List<String>(requestHeaderWhitelist ?? new List<String>());
            this.ResponseHeaderWhitelist = new List<String>(responseHeaderWhitelist ?? new List<String>());
        }

        public void Initialize(ITelemetry telemetry)
        {
            HttpContext CurrentContext = HttpContext.Current;

            if (CurrentContext == null)
            {
                return;
            }

            if (CurrentContext.Request != null)
            {
                foreach (String RequestHeaderName in CurrentContext.Request.Headers.AllKeys)
                {
                    if (this.RequestHeaderWhitelist != null && this.RequestHeaderWhitelist.Count > 0 && !this.RequestHeaderWhitelist.Any(x => String.Equals(x, RequestHeaderName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }

                    String HeaderValue = CurrentContext.Request.Headers[RequestHeaderName];

                    if (!String.IsNullOrWhiteSpace(HeaderValue))
                    {
                        telemetry.Context.GlobalProperties.Add($"request-{RequestHeaderName}", HeaderValue);
                    }
                }
            }

            if (CurrentContext.Response != null)
            {
                foreach (String ResponseHeaderName in CurrentContext.Response.Headers.AllKeys)
                {
                    if (this.ResponseHeaderWhitelist != null && this.ResponseHeaderWhitelist.Count > 0 && !this.ResponseHeaderWhitelist.Any(x => String.Equals(x, ResponseHeaderName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }

                    String HeaderValue = CurrentContext.Response.Headers[ResponseHeaderName];

                    if (!String.IsNullOrWhiteSpace(HeaderValue))
                    {
                        telemetry.Context.GlobalProperties.Add($"response-{ResponseHeaderName}", HeaderValue);
                    }
                }
            }
        }
    }
}