using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Infra.Context;
using AUTHIO.APPLICATION.Infra.Repository.Base;

namespace AUTHIO.APPLICATION.Infra.Repository;

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
            new TenantUserAdminEntity { TenantId = tenantId, UserId = userId });
}
