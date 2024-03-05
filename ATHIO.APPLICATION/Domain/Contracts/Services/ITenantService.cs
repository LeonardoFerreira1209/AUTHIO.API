using AUTHIO.APPLICATION.Domain.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.APPLICATION.Domain.Contracts.Services;

/// <summary>
/// Interface de Tenants.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Método responsável por criar um tenant.
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <returns></returns>
    Task<ObjectResult> CreateAsync(
        CreateTenantRequest createTenantRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Método responsável por criar um usuário no tenant.
    /// </summary>
    /// <param name="registerTenantUserRequest"></param>
    /// <param name="tenantId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> RegisterTenantUserAsync(
        RegisterTenantUserRequest registerTenantUserRequest, Guid tenantId, CancellationToken cancellationToken);
}
