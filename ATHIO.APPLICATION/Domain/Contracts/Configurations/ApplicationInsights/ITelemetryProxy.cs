namespace AUTHIO.APPLICATION.Domain.Contracts.Configurations.ApplicationInsights;

public interface ITelemetryProxy
{
    void TrackEvent(string eventName);
}

