using AUTHIO.API.Controllers.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers.Tenants;

/// <summary>
/// Tenant controller.
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="featureFlags"></param>
/// <param name="unitOfWork"></param>
public sealed class TenantController(
    IFeatureFlags featureFlags, IUnitOfWork unitOfWork) 
        : BaseControllercs(featureFlags, unitOfWork)
{
    /// <summary>
    /// Endpoint responsável pela criação de tenants e usuário inicial.
    /// </summary>
    /// <param name="tenantCreateRequest"></param>
    /// <returns></returns>
    [HttpPost("create/tenant")]
    //[CustomAuthorize(Claims.Chat, "Post")]
    [SwaggerOperation(Summary = "Recuperar dados do chat", Description = "Método responsável por recuperar dados do chat")]
    //[ProducesResponseType(typeof(ApiResponse<ChatResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse<ChatResponse>), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse<ChatResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTenantAsync(TenantCreateRequest tenantCreateRequest)
    {
        using (LogContext.PushProperty("Controller", "TenantController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(tenantCreateRequest)))
        using (LogContext.PushProperty("Metodo", "CreateTenantAsync"))
        {
            return null;
        }
    }
}
