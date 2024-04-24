using AUTHIO.DOMAIN.Contracts.Configurations.ApplicationInsights;
using Microsoft.ApplicationInsights;
using System.Diagnostics.CodeAnalysis;

namespace AUTHIO.APPLICATION.Configurations.ApplicationInsights;

[ExcludeFromCodeCoverage]
public sealed class TelemetryProxy : ITelemetryProxy
{
    private readonly TelemetryClient _telemetryClient;

    public TelemetryProxy(TelemetryClient telemetryClient) => _telemetryClient = telemetryClient;

    public void TrackEvent(string eventName) => _telemetryClient.TrackEvent(eventName);
}
