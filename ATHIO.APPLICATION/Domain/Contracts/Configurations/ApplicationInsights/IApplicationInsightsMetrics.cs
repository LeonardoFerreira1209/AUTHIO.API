using AUTHIO.APPLICATION.Domain.Dtos.Configurations;

namespace AUTHIO.APPLICATION.Domain.Contracts.Configurations.ApplicationInsights;

public interface IApplicationInsightsMetrics
{
    void AddMetric(CustomMetricDto metrica);
}
