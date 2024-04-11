using AUTHIO.API.Controllers.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.TOKEN;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Controller que cuida do fluxo de autenticação.
/// </summary>
/// <param name="featureFlags"></param>
public class AuthenticationController(
    IFeatureFlags featureFlags, IAuthenticationService authenticationService, IUnitOfWork<AuthIoContext> unitOfWork) 
        : BaseController(featureFlags, unitOfWork)
{
    private readonly IAuthenticationService 
        _authenticationService = authenticationService;

    /// <summary>
    /// Endpoint responsável pelo registro de usuários no sistema.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Registrar usuário", Description = "Método responsável por registrar um usuário no sistema!")]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
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
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    [HttpGet("authetication")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Autenticação do usuário", Description = "Endpoint responsável por fazer a autenticação do usuário, é retornado um token JWT (Json Web Token).")]
    [ProducesResponseType(typeof(ApiResponse<TokenJWT>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<LoginRequest>), StatusCodes.Status423Locked)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AuthenticationAsync(
        [FromHeader][Required] string username,
        [FromHeader][Required] string password, [FromHeader] string apiKey)
    {
        using (LogContext.PushProperty("Controller", "UserController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(new { username, password, apiKey })))
        using (LogContext.PushProperty("Metodo", "Authentication"))
        {
            return await ExecuteAsync(nameof(AuthenticationAsync),
                () => _authenticationService.AuthenticationAsync(
                    new LoginRequest(username, password)), "Autenticar usuário");
        }
    }
}
