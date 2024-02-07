using AUTHIO.API.Controllers.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Tenant controller.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="featureFlags"></param>
/// <param name="unitOfWork"></param>
public sealed class TenantController(
    IFeatureFlags featureFlags, IUnitOfWork unitOfWork, ITenantService tenantService)
        : BaseController(featureFlags, unitOfWork)
{
    private readonly ITenantService _tenantService = tenantService;

    /// <summary>
    /// Endpoint responsável pela criação de tenants e usuário inicial.
    /// </summary>
    /// <param name="tenantCreateRequest"></param>
    /// <returns></returns>
    [HttpPost("create/tenant")]
    [SwaggerOperation(Summary = "Criar tenant e usuário responsável", Description = "Método responsável por Criar tenant e usuário responsável!")]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<TenantResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTenantAsync([FromBody] TenantProvisionRequest tenantCreateRequest)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(tenantCreateRequest)))
        using (LogContext.PushProperty("Metodo", "CreateTenantAsync"))
        {
            return await ExecuteAsync(nameof(CreateTenantAsync),
                 () => _tenantService.TenantProvisionAsync(tenantCreateRequest), "Criar tenant e usuário responsável");
        }
    }
}
