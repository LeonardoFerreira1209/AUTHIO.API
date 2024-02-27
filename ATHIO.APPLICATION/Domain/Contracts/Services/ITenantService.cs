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
}
