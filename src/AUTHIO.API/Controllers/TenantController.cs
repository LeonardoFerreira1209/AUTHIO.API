﻿using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.CustomAuthorize.Attribute;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Enums;
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
/// <param name="tenantService"></param>
[Controller]
[Route("tenant/api")]
public class TenantController(
    IFeatureFlagsService featureFlags, ITenantService tenantService)
        : BaseController(featureFlags)
{
    private readonly ITenantService
        _tenantService = tenantService;

    /// <summary>
    /// Endpoint responsável pelo registro de tenant.
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("registrar")]
    [EnableRateLimiting("fixed")]
    [CustomAuthorize(Claims.Tenants, "POST")]
    [SwaggerOperation(Summary = "Registrar tenant", Description = "Método responsável por registrar um tenant!")]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateTenantRequest createTenantRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(createTenantRequest)))
        using (LogContext.PushProperty("Metodo", "CreateAsync"))
        {
            return await ExecuteAsync(nameof(CreateAsync),
                 () => _tenantService.CreateAsync(
                     createTenantRequest, cancellationToken), "Registrar tenant.");
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar os tenants do usuário logado.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("recuperar/todos")]
    [EnableRateLimiting("fixed")]
    [CustomAuthorize(Claims.Tenants, "GET")]
    [SwaggerOperation(Summary = "Buscar todos os tenants", Description = "Método responsável por buscar todos os tenants do usuário!")]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Metodo", "CreateAsync"))
        {
            return await ExecuteAsync(nameof(GetAllAsync),
                 () => _tenantService.GetAllAsync(
                     pageNumber, pageSize, cancellationToken), "Buscar todos os tenants.");
        }
    }

    /// <summary>
    /// Endpoint responsável pelo registro de usuário em um tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("registrar/usuario")]
    [EnableRateLimiting("fixed")]
    [CustomAuthorize(Claims.Tenants, "PUT")]
    [SwaggerOperation(Summary = "Registrar usuário no tenant", Description = "Método responsável por registrar um usuário no tenant!")]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterTenantUserAsync(
        [FromBody] RegisterUserRequest registerUserRequest, [FromHeader] string tenantKey, CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(registerUserRequest)))
        using (LogContext.PushProperty("Metodo", "RegisterTenantUserAsync"))
        {
            return await ExecuteAsync(nameof(RegisterTenantUserAsync),
                 () => _tenantService.RegisterTenantUserAsync(
                     registerUserRequest, tenantKey, cancellationToken), "Registrar usuário no tenant.");
        }
    }
}
