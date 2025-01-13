using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers.OIDC;

/// <summary>
/// Controller que cuida do fluxo de open id connect.
/// </summary>
[ApiController]
[ControllerName("Open-Id-Connect")]
public class OIDCController(
        IOpenConnectService openConnectService
    ) : ControllerBase
{
    /// <summary>
    ///  Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <returns></returns>
    [HttpGet(".well-known/openid-configuration")]
    [EnableRateLimiting("default-fixed-window")]
    [SwaggerOperation(
        Summary = "Busca dados do open id connect.",
        Description = "Método responsável por retornar os dados do open id connect."
    )]
    public async Task<OpenIdConnectConfiguration> OpenIdConfigurationAsync()
    {
        using (LogContext.PushProperty("Controller", nameof(OIDCController)))
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
    [EnableRateLimiting("default-fixed-window")]
    [SwaggerOperation(
        Summary = "Busca as chaves de segurança.",
        Description = "Método as chaves de segurança."
    )]
    public async Task<object> GetJwksAsync()
    {
        using (LogContext.PushProperty("Controller", nameof(OIDCController)))
        using (LogContext.PushProperty("Metodo", nameof(GetJwksAsync)))
        {
            return await openConnectService
                .GetJwksAsync();
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <returns></returns>
    [HttpGet("clients/{x-Client-key}/.well-known/openid-configuration")]
    [EnableRateLimiting("default-fixed-window")]
    [SwaggerOperation(
        Summary = "Busca dados do open id connect baseado no tenatKey.",
        Description = "Método responsável por retornar os dados do open id connect por Client key."
    )]
    public async Task<OpenIdConnectConfiguration> OpenIdConfigurationAsync(
        [FromRoute(Name = "x-Client-key")] string clientKey
        )
    {
        using (LogContext.PushProperty("Controller", nameof(OIDCController)))
        using (LogContext.PushProperty("Metodo", nameof(OpenIdConfigurationAsync)))
        {
            return await openConnectService
                .GetOpenIdConnectConfigurationAsync(
                    clientKey
                );
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <param name="ClientKey"></param>
    /// <returns></returns>
    [HttpGet("clients/{x-Client-key}/jwks")]
    [EnableRateLimiting("default-fixed-window")]
    [SwaggerOperation(
        Summary = "Busca dados das chaves de segurança por Client key.",
        Description = "Método responsável por retornar chaves de segurança por Client key."
    )]
    public async Task<object> GetJwksAsync(
        [FromRoute(Name = "x-Client-key")] string clientKey
        )
    {
        using (LogContext.PushProperty("Controller", nameof(OIDCController)))
        using (LogContext.PushProperty("Metodo", nameof(GetJwksAsync)))
        {
            return await openConnectService
                .GetJwksAsync(
                    clientKey
                );
        };
    }
}
