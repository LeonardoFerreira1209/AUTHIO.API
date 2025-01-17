﻿using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

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

    /// <summary>
    /// Verifica se um tenant existe baseado na tenant key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> ExistsByKey(string key);
}
