using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de TenantIdentityConfiguration.
/// </summary>
public class TenantIdentityConfigurationBuilder
{
    private Guid tenantConfigurationId;
    private DateTime? updated = null;
    private DateTime created;

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
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantIdentityConfigurationEntity Builder()
        => new(tenantConfigurationId, created, updated);
}
