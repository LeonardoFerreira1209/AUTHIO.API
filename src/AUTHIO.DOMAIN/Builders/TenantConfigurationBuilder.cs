using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de TenantConfiguration.
/// </summary>
public sealed class TenantConfigurationBuilder
{
    private string tenantKey;
    private Guid tenantId;
    private DateTime? updated = null;
    private DateTime created;
    private TenantIdentityConfigurationEntity tenantIdentityConfiguration;
    private TenantEmailConfigurationEntity tenantEmailConfiguration;
    private TenantTokenConfigurationEntity tenantTokenConfiguration;

    /// <summary>
    /// Adiciona uma tenantKey.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddTenantKey(string tenantKey)
    {
        this.tenantKey = tenantKey;

        return this;
    }

    /// <summary>
    /// Adiciona um TenantId.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddTenantId(Guid tenantId)
    {
        this.tenantId = tenantId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddCreated(DateTime? created = null)
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
    public TenantConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona Tenant identity configuration.
    /// </summary>
    /// <param name="tenantIdentityConfiguration"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddTenantIdentityConfiguration(TenantIdentityConfigurationEntity tenantIdentityConfiguration)
    {
        this.tenantIdentityConfiguration = tenantIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Tenant email configuration.
    /// </summary>
    /// <param name="tenantEmailConfiguration"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddTenantEmailConfiguration(TenantEmailConfigurationEntity tenantEmailConfiguration)
    {
        this.tenantEmailConfiguration = tenantEmailConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Tenant token configuration.
    /// </summary>
    /// <param name="tenantTokenConfiguration"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddTenantTokenConfiguration(TenantTokenConfigurationEntity tenantTokenConfiguration)
    {
        this.tenantTokenConfiguration = tenantTokenConfiguration;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantConfigurationEntity Builder()
        => new(tenantKey, tenantId, 
            created, updated,
            tenantIdentityConfiguration, 
            tenantEmailConfiguration, 
            tenantTokenConfiguration
        );
}
