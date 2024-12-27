using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.Token;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Controller que cuida do fluxo de autenticação.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="authenticationService"></param>
[ApiController]
[Route("api/authentications")]
public class AuthenticationController(
    IFeatureFlagsService featureFlags, 
    IAuthenticationService authenticationService) : BaseController(featureFlags)
{
    private readonly IAuthenticationService
        _authenticationService = authenticationService;

    /// <summary>
    /// Endpoint responsável por fazer a autenticação do usuário, é retornado um token JWT (Json Web Token).
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpGet("signin")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Autenticação do usuário", 
        Description = "Endpoint responsável por fazer a autenticação do usuário, é retornado um token JWT (Json Web Token)."
    )]
    [ProducesResponseType(typeof(ApiResponse<TokenJWT>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status423Locked)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SigninAsync(
        string username, string password)
    {
        using (LogContext.PushProperty("Controller", nameof(AuthenticationController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(new { username, password })))
        using (LogContext.PushProperty("Metodo", nameof(SigninAsync)))
        {
            return await ExecuteAsync(nameof(SigninAsync),
                () => _authenticationService.AuthenticationAsync(
                    new LoginRequest(
                        username,
                        password
                    ),
                    null
                ), "Autenticar usuário"
            );
        }
    }

    /// <summary>
    /// Endpoint responsável por fazer a autenticação do usuário baseado em um tenant, é retornado um token JWT (Json Web Token).
    /// </summary>
    /// <param name="authenticationRequest"></param>
    /// <returns></returns>
    [HttpGet("tenants/{x-tenant-key}/signin")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Autenticação do usuário",
        Description = "Endpoint responsável por fazer a autenticação do usuário baseado em um tenant, é retornado um token JWT (Json Web Token)."
    )]
    [ProducesResponseType(typeof(ApiResponse<TokenJWT>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status423Locked)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SigninByTenantAsync(
        [FromRoute(Name = "x-tenant-key")] string tenantKey, 
        AuthenticationRequest authenticationRequest
        )
    {
        using (LogContext.PushProperty("Controller", nameof(AuthenticationController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(authenticationRequest)))
        using (LogContext.PushProperty("Metodo", nameof(SigninByTenantAsync)))
        {
            return await ExecuteAsync(nameof(SigninAsync),
                () => _authenticationService.AuthenticationAsync(
                    new LoginRequest(
                        authenticationRequest.Username,
                        authenticationRequest.Password
                    ),
                    tenantKey
                ), "Autenticar usuário"
            );
        }
    }
}
