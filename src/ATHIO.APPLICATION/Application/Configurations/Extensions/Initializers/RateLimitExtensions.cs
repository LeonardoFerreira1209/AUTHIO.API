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
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = 429;
            options.AddFixedWindowLimiter("fixed", options =>
            {
                options.AutoReplenishment = true;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 2;
                options.PermitLimit = 10;
                options.Window = TimeSpan.FromMinutes(3);
            });
        });

        return services;
    }
}
