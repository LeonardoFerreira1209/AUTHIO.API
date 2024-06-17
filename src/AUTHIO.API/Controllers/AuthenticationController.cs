using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.Token;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
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
[ApiController]
[Route("authentication/api")]
public class AuthenticationController(
    IFeatureFlagsService featureFlags, IAuthenticationService authenticationService)
        : BaseController(featureFlags)
{
    private readonly IAuthenticationService
        _authenticationService = authenticationService;

    /// <summary>
    /// Endpoint responsável pelo registro de usuários no sistema.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("registrar/usuario")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Registrar usuário", Description = "Método responsável por registrar um usuário no sistema!")]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "AuthenticationController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(registerUserRequest)))
        using (LogContext.PushProperty("Metodo", "RegisterAsync"))
        {
            return await ExecuteAsync(nameof(RegisterAsync),
                 () => _authenticationService.RegisterAsync(
                     registerUserRequest, cancellationToken), "Registrar usuário no sistema.");
        }
    }

    /// <summary>
    /// Endpoint responsável por fazer a autenticação do usuário, é retornado um token JWT (Json Web Token).
    /// </summary>
    /// <param name="authenticationRequest"></param>
    /// <returns></returns>
    [HttpGet("autenticar/usuario")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Autenticação do usuário", Description = "Endpoint responsável por fazer a autenticação do usuário, é retornado um token JWT (Json Web Token).")]
    [ProducesResponseType(typeof(ApiResponse<TokenJWT>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status423Locked)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AuthenticationAsync(
        AuthenticationRequest authenticationRequest)
    {
        using (LogContext.PushProperty("Controller", "UserController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(authenticationRequest)))
        using (LogContext.PushProperty("Metodo", "Authentication"))
        {
            return await ExecuteAsync(nameof(AuthenticationAsync),
                () => _authenticationService.AuthenticationAsync(
                    new LoginRequest(authenticationRequest.Username, 
                        authenticationRequest.Password)), "Autenticar usuário");
        }
    }
}
