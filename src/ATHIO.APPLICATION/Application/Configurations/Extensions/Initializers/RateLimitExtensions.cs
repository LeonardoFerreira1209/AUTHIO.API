using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;

/// <summary>
/// Rate limit extensions
/// </summary>
public static class RateLimitExtensions
{
    /// <summary>
    /// Configuração de Rate Limit.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureFixedRateLimit(
        this IServiceCollection services)
    {
        services.AddRateLimiter(_ => 
            _.AddFixedWindowLimiter(policyName: "fixed", options => {

                options.PermitLimit = 10;
                options.Window = TimeSpan.FromMinutes(10);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 5;

            }));

        return services;
    }
}
