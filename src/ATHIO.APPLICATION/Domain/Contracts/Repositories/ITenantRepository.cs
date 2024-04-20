using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories;

/// <summary>
/// Repositorio de Tenants.
/// </summary>
public interface ITenantRepository
    : IGenerictEntityCoreRepository<TenantEntity>
{
    /// <summary>
    /// Vincula um usuário admin e um tenant.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task LinkTenantWithUserAdminAsync(Guid tenantId, Guid userId);
}
