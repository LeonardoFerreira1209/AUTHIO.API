using AUTHIO.APPLICATION.Domain.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.APPLICATION.Domain.Contracts.Services;

/// <summary>
/// Interface de Tenants.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Método responsável por criar e configurar um tenant.
    /// </summary>
    /// <param name="tenantProvisionRequest"></param>
    /// <returns></returns>
    Task<ObjectResult> TenantProvisionAsync(
        TenantProvisionRequest tenantProvisionRequest);
}
