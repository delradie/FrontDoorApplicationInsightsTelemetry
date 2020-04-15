using Mercato.ApplicationInsights.Web.FrontDoor;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TelemetryTestSite
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            List<String> RequestHeaders = new List<string>()
            {
                "User-Agent"
            };

            TelemetryConfiguration.Active.TelemetryInitializers.Add(new HttpHeaderTelemetryInitializer(RequestHeaders, null));
        }
    }
}