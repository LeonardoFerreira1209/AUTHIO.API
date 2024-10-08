﻿using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um LockoutIdentityConfigurationEntity.
/// </summary>
public static class CreateLockoutIdentityConfiguration
{
    /// <summary>
    /// Cria uma instância de LockoutIdentityConfigurationEntity padrão.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="allowedForNewUsers"></param>
    /// <param name="maxFailedAccessAttempts"></param>
    /// <param name="defaultLockoutTimeSpan"></param>
    /// <returns></returns>
    public static LockoutIdentityConfigurationEntity CreateDefault(Guid tenantIdentityConfigurationId)
            => new LockoutIdentityConfigurationBuilder()
                .AddTenantConfigurationId(tenantIdentityConfigurationId)
                .AddCreated(DateTime.Now)
                .AddAllowedForNewUsers(true)
                .AddMaxFailedAccessAttempts(3)
                .AddDefaultLockoutTimeSpan(TimeSpan.FromMinutes(5))
                .Builder();
}
