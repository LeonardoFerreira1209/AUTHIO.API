using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AUTHIO.APPLICATION.Middlewares;

/// <summary>
/// 
/// </summary>
/// <param name="requestDelegate"></param>
public class JwtServiceDiscoveryMiddleware(
    RequestDelegate requestDelegate
    )
{
    /// <summary>
    /// Método de invocação de Handler.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="keyService"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task InvokeAsync(
        HttpContext httpContext,
        IJwtService keyService,
        IOptions<JwtOptions> options)
    {
        try
        {
            var storedKeys = await keyService.GetLastKeys(
                options.Value.AlgorithmsToKeep
            );

            var keys = new
            {
                keys = storedKeys.Select(
                    s => s.GetSecurityKey()
                ).Select(
                    PublicJsonWebKey.FromJwk
                )
            };

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(
                    keys
                )
            );

            await requestDelegate(httpContext);
        }
        catch
        {
            throw;
        }
    }
}