using AUTHIO.API.Controllers.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Registrar usuário", Description = "Método responsável por registrar um usuário no sistema!")]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "AuthenticationController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(registerUserRequest)))
        using (LogContext.PushProperty("Metodo", "RegisterAsync"))
        {
            return await ExecuteAsync(nameof(RegisterAsync),
                 () => _authenticationService.RegisterAsync(registerUserRequest, cancellationToken), "Registrar usuário no sistema.");
        }
    }
}
