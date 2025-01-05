using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Threading.RateLimiting;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

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
            options.AddPolicy("default-fixed-window",
                context => RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.User.Identity.Name ?? "anonymous",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2,
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(3)
                    }
                )
            );

            options.AddPolicy("default-token-bucket-window",
                context => RateLimitPartition.GetTokenBucketLimiter(
                    partitionKey: context.User.Identity.Name ?? "anonymous",
                    factory: _ => new TokenBucketRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 2,
                        TokenLimit = 10,
                        TokensPerPeriod = 5,
                        ReplenishmentPeriod = TimeSpan.FromMinutes(1)
                    }
                )
            );

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode
                    = StatusCodes.Status429TooManyRequests;

                var response = new ObjectResponse(
                    HttpStatusCode.TooManyRequests,
                    new ApiResponse<UserResponse>(
                        false,
                        HttpStatusCode.TooManyRequests,
                        null, [
                            new DataNotifications("Excedido o limite de requests por minuto! Aguarde...")
                        ]
                    )
                );
                
                var jsonResponse = JsonConvert
                    .SerializeObject(
                        response
                    );

                await context.HttpContext.Response
                    .WriteAsync(
                        jsonResponse, 
                        cancellationToken
                    );
            };
        });

        return services;
    }
}
