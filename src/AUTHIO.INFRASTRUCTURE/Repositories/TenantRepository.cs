using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Tenants.
/// </summary>
public sealed class TenantRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantEntity>(context), ITenantRepository
{
    private readonly AuthIoContext _context = context;

    /// <summary>
    /// Vincula um usuário admin e um tenant.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task LinkTenantWithUserAdminAsync(Guid tenantId, Guid userId)
        => await _context.AddAsync(
            new TenantIdentityUserAdminEntity { TenantId = tenantId, UserId = userId });
}
