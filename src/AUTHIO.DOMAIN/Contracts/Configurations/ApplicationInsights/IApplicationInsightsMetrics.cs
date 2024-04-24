using AUTHIO.DOMAIN.Dtos.Configurations;

namespace AUTHIO.DOMAIN.Contracts.Configurations.ApplicationInsights;

public interface IApplicationInsightsMetrics
{
    void AddMetric(CustomMetricDto metrica);
}
