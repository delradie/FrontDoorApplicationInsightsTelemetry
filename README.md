Adds additional utilities for Azure Frontdoor in Application Insights

Mercato.ApplicationInsights.Web.FrontDoor
=========================================

Adds a telemetry initialiser to mark blank User Agent strings as synthetic.

This is needed as Front Door health probe requests have no User Agent and can flood the AI telemetry stream with several requests a second.

To activate, add the following line to your ApplicationInsights.config under the TelemetryInitializers element:

```xml
<Add Type="Mercato.ApplicationInsights.Web.FrontDoor.SyntheticBlankUserAgentTelemetryInitializer, Mercato.ApplicationInsights.Web.FrontDoor" />
```

Also adds a telemetry initialiser that allows for the logging of Request and Resposne Http Headers to telemetry properties. Especially useful when needing to examine the "X-Forwarded-Host" header for requests forwarded by FrontDoor

There are 2 publicly updateable String lists to allow for the setting of a whitelise of headers that you would like reported

This can be initialised in ApplicationInsights.config, or in Application_Start in global.asax, to allow for passing in of initial values for the whitelists:

```csharp
            List<String> RequestHeaders = new List<string>()
            {
                "User-Agent"
            };

            TelemetryConfiguration.Active.TelemetryInitializers.Add(new HttpHeaderTelemetryInitializer(RequestHeaders, null));

```

Published as Nuget - [Mercato.ApplicationInsights.Web.FrontDoor](https://www.nuget.org/packages/Mercato.ApplicationInsights.Web.FrontDoor/)
