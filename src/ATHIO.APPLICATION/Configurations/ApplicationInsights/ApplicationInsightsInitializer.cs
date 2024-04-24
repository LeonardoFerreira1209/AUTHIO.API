using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace AUTHIO.APPLICATION.Configurations.ApplicationInsights;

public sealed class ApplicationInsightsInitializer(
    IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : ITelemetryInitializer
{
    private readonly IHttpContextAccessor
        _httpContextAccessor = httpContextAccessor;

    private readonly string _instrumentationKey
        = configuration.GetSection("ApplicationInsights:InstrumentationKey").Value;

    private readonly string _roleName
        = configuration.GetSection("ApplicationInsights:CloudRoleName").Value;

    /// <summary>
    /// Inicializa as telemetry do appinsights.
    /// </summary>
    /// <param name="telemetry"></param>
    public void Initialize(ITelemetry telemetry)
    {
        telemetry.Context.InstrumentationKey = _instrumentationKey;

        telemetry.Context.Cloud.RoleName = _roleName;

        if (_httpContextAccessor.HttpContext != null)
        {
            foreach (var prop in _httpContextAccessor.HttpContext.Items)
                if (prop.Key is string)
                    telemetry.Context.GlobalProperties[prop.Key.ToString()] = prop.Value.ToString();
        }
    }
}

