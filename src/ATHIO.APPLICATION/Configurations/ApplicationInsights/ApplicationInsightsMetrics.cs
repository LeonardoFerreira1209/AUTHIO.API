using AUTHIO.DOMAIN.Contracts.Configurations.ApplicationInsights;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Diagnostics.CodeAnalysis;

namespace AUTHIO.APPLICATION.Configurations.ApplicationInsights;

[ExcludeFromCodeCoverage]
public sealed class ApplicationInsightsMetrics : IApplicationInsightsMetrics
{
    private readonly TelemetryClient _telemetry;

    public ApplicationInsightsMetrics(TelemetryClient telemetry, string key)
    {
        _telemetry = telemetry;

        _telemetry.Context.InstrumentationKey = key;
    }

    /// <summary>
    /// Adiciona as métricas.
    /// </summary>
    /// <param name="metrica"></param>
    public void AddMetric(CustomMetricDto metrica)
    {

        if (metrica != null)
        {
            _telemetry.TrackMetric(new MetricTelemetry
            {
                Name = metrica.NomeMetrica,
                Sum = metrica.ValorMetrica
            });

        }

    }

}


