using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers.OpenConnect;

/// <summary>
/// Controller que cuida do fluxo de open id connect.
/// </summary>
[ApiController]
public class OpenConnectController(
        IOpenConnectService openConnectService
    ) : ControllerBase
{
    /// <summary>
    ///  Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    [HttpGet(".well-known/openid-configuration")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Busca dados do open id connect.",
        Description = "Método responsável por retornar os dados do open id connect."
    )]
    public async Task<IActionResult> OpenIdConfigurationAsync()
    {
        using (LogContext.PushProperty("Controller", nameof(OpenConnectController)))
        using (LogContext.PushProperty("Metodo", nameof(OpenIdConfigurationAsync)))
        {
            return await openConnectService
                .GetOpenIdConnectConfigurationAsync();
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar a chaves de segurança.
    /// </summary>
    /// <returns></returns>
    [HttpGet("jwks")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Busca as chaves de segurança.",
        Description = "Método as chaves de segurança."
    )]
    public async Task<object> GetJwksAsync()
    {
        using (LogContext.PushProperty("Controller", nameof(OpenConnectController)))
        using (LogContext.PushProperty("Metodo", nameof(GetJwksAsync)))
        {
            return await openConnectService
                .GetJwksAsync();
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    [HttpGet("tenants/{x-tenant-key}/.well-known/openid-configuration")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Busca dados do open id connect baseado no tenatKey.",
        Description = "Método responsável por retornar os dados do open id connect por tenant key."
    )]
    public async Task<IActionResult> OpenIdConfigurationAsync(
        [FromRoute(Name = "x-tenant-key")] string tenantKey
        )
    {
        using (LogContext.PushProperty("Controller", nameof(OpenConnectController)))
        using (LogContext.PushProperty("Metodo", nameof(OpenIdConfigurationAsync)))
        {
            return await openConnectService
                .GetOpenIdConnectConfigurationAsync(
                    tenantKey
                );
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    [HttpGet("tenants/{x-tenant-key}/jwks")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Busca dados das chaves de segurança por tenant key.",
        Description = "Método responsável por retornar chaves de segurança por tenant key."
    )]
    public async Task<object> GetJwksAsync(
        [FromRoute(Name = "x-tenant-key")] string tenantKey
        )
    {
        using (LogContext.PushProperty("Controller", nameof(OpenConnectController)))
        using (LogContext.PushProperty("Metodo", nameof(GetJwksAsync)))
        {
            return await openConnectService
                .GetJwksAsync(
                    tenantKey
                );
        };
    }
}
