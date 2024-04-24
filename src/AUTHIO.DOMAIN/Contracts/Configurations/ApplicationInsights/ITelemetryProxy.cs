namespace AUTHIO.DOMAIN.Contracts.Configurations.ApplicationInsights;

public interface ITelemetryProxy
{
    void TrackEvent(string eventName);
}

