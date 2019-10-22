Adds additional utilities for Azure Frontdoor in Application Insights

Mercato.ApplicationInsights.Web.FrontDoor
=========================================

Adds a telemetry initialiser to mark blank User Agent strings as synthetic.

This is needed as Front Door health probe requests have no User Agent and can flood the AI telemetry stream with several requests a second.

To activate, add the following line to your ApplicationInsights.config under the TelemetryInitializers element:

```xml
<Add Type="Mercato.ApplicationInsights.Web.FrontDoor.SyntheticBlankUserAgentTelemetryInitializer, Mercato.ApplicationInsights.Web.FrontDoor" />
```