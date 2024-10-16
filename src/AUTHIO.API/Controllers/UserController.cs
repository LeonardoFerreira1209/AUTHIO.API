﻿using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.CustomAuthorize.Attribute;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
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
/// Controller que cuida do fluxo de usuários.
/// </summary>
/// <param name="featureFlags"></param>
[ApiController]
[Route("api/users")]
public class UserController(
    IFeatureFlagsService featureFlags, IUserService userService)
        : BaseController(featureFlags)
{
    /// <summary>
    /// Endpoint responsável pelo registro de usuários no sistema.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Registrar usuário", Description = "Método responsável por registrar um usuário no sistema!")]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "UserController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(registerUserRequest)))
        using (LogContext.PushProperty("Metodo", "RegisterAsync"))
        {
            return await ExecuteAsync(nameof(RegisterAsync),
                 () => userService.RegisterAsync(
                     registerUserRequest, cancellationToken), "Registrar usuário no sistema.");
        }
    }

    /// <summary>
    /// Endpoint responsável pelo retorno de usuário por Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Recuperar usuário", Description = "Método responsável por buscar um usuário por id!")]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Tetsesync(
       Guid id, 
       CancellationToken cancellationToken)
    {
        return await ExecuteAsync(nameof(RegisterAsync),
                () => userService.GetUserByIdAsync(
                    id, cancellationToken), "Recuperar usuário por id.");
    }
}
