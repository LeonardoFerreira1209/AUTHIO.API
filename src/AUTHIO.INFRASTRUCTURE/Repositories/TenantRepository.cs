using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Verifica se um tenant existe baseado na tenant key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> ExistsByKey(string key)
    {
        var exists = await _context.Tenants.FirstOrDefaultAsync(
            x => x.TenantConfiguration
                .TenantKey.Equals(key)
        ) != null;

        return exists;
    }
}
