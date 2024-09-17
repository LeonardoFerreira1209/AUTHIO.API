using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração de identity do tenant.
/// </summary>
public static class CreateTenantIdentityConfiguration
{
    /// <summary>
    /// Cria um tenant identity configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <param name="userIdentityConfiguration"></param>
    /// <param name="passwordIdentityConfiguration"></param>
    /// <param name="lockoutIdentityConfiguration"></param>
    /// <returns></returns>
    public static TenantIdentityConfigurationEntity CreateDefault(Guid tenantConfigurationId,
        UserIdentityConfigurationEntity userIdentityConfiguration,
        PasswordIdentityConfigurationEntity passwordIdentityConfiguration,
        LockoutIdentityConfigurationEntity lockoutIdentityConfiguration)
    => new TenantIdentityConfigurationBuilder()
        .AddTenantConfigurationId(tenantConfigurationId)
        .AddCreated()
        .AddUserIdentityConfiguration(userIdentityConfiguration)
        .AddPasswordIdentityConfiguration(passwordIdentityConfiguration)
        .AddLockoutIdentityConfiguration(lockoutIdentityConfiguration)
        .Builder();
}
