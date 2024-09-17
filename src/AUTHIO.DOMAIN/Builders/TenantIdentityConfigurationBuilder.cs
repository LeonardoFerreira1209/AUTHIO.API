using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de TenantIdentityConfiguration.
/// </summary>
public sealed class TenantIdentityConfigurationBuilder
{
    /// <summary>
    /// Private properties.
    /// </summary>
    private Guid tenantConfigurationId;
    private DateTime? updated = null;
    private DateTime created;
    private UserIdentityConfigurationEntity userIdentityConfiguration;
    private PasswordIdentityConfigurationEntity passwordIdentityConfiguration;
    private LockoutIdentityConfigurationEntity lockoutIdentityConfiguration;

    /// <summary>
    /// Adiciona um Tenant configuration Id.
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddTenantConfigurationId(Guid tenantConfigurationId)
    {
        this.tenantConfigurationId = tenantConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddCreated(DateTime? created = null)
    {
        this.created
            = created
            ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona a data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona Identity user configuration.
    /// </summary>
    /// <param name="userIdentityConfiguration"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddUserIdentityConfiguration(UserIdentityConfigurationEntity userIdentityConfiguration)
    {
        this.userIdentityConfiguration = userIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Identity password configuration.
    /// </summary>
    /// <param name="passwordIdentityConfiguration"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddPasswordIdentityConfiguration(PasswordIdentityConfigurationEntity passwordIdentityConfiguration)
    {
        this.passwordIdentityConfiguration = passwordIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Identity lockout configuration.
    /// </summary>
    /// <param name="lockoutIdentityConfiguration"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddLockoutIdentityConfiguration(LockoutIdentityConfigurationEntity lockoutIdentityConfiguration)
    {
        this.lockoutIdentityConfiguration = lockoutIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantIdentityConfigurationEntity Builder()
        => new(tenantConfigurationId, 
            created, 
            updated,
            userIdentityConfiguration,
            passwordIdentityConfiguration,
            lockoutIdentityConfiguration
        );
}
