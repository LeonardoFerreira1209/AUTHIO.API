using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.CustomAuthorize.Attribute;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
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
[Route("api/tenants")]
public class TenantController(
    IFeatureFlagsService featureFlags, 
    ITenantService tenantService) : BaseController(featureFlags)
{
    private readonly ITenantService
        _tenantService = tenantService;

    /// <summary>
    /// Endpoint responsável pelo registro de tenant.
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableRateLimiting("fixed")]
    [Authorize(Claims.Tenants, "POST")]
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
    /// Endpoint responsável pela atualização de um tenant.
    /// </summary>
    /// <param name="updateTenantRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [EnableRateLimiting("fixed")]
    [Authorize(Claims.Tenants, "PUT")]
    [SwaggerOperation(
        Summary = "Atualizar tenant", 
        Description = "Método responsável por atualizar um tenant!"
    )]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateTenantRequest updateTenantRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(updateTenantRequest)))
        using (LogContext.PushProperty("Metodo", "UpdateAsync"))
        {
            return await ExecuteAsync(nameof(UpdateAsync),
                 () => _tenantService.UpdateAsync(
                     updateTenantRequest, cancellationToken), "Atualizar tenant.");
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar os tenants do usuário logado.
    /// </summary>
    /// <param name="filterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [EnableRateLimiting("fixed")]
    [Authorize(Claims.Tenants, "GET")]
    [SwaggerOperation(
        Summary = "Buscar todos os tenants", 
        Description = "Método responsável por buscar todos os tenants do usuário!"
    )]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(
       [FromQuery] FilterRequest filterRequest, 
       CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Metodo", "CreateAsync"))
        {
            return await ExecuteAsync(nameof(GetAllAsync),
                 () => _tenantService.GetAllAsync(
                     filterRequest, cancellationToken), "Buscar todos os tenants.");
        }
    }

    /// <summary>
    /// Endpoint responsável pelo registro de usuário em um tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("users/register")]
    [EnableRateLimiting("fixed")]
    [Authorize(Claims.Tenants, "PATCH")]
    [SwaggerOperation(
        Summary = "Registrar usuário no tenant", 
        Description = "Método responsável por registrar um usuário no tenant!"
    )]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterTenantUserAsync(
        [FromHeader(Name = "X-Tenant-KEY")] string tenantKey, 
        [FromBody] RegisterUserRequest registerUserRequest, 
        CancellationToken cancellationToken)
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
