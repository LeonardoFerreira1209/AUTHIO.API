using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Entity;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repository;

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
