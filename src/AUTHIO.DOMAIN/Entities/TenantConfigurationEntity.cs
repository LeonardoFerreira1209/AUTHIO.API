namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de configuração de Tenanty.
/// </summary>
public class TenantConfigurationEntity : IEntityPrimaryKey<Guid>, IEntityTenant
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <param name="tenantId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public TenantConfigurationEntity(
        string tenantKey, Guid tenantId,
        DateTime created, DateTime? updated)
    {
        TenantKey = tenantKey;
        TenantId = tenantId;
        Created = created;
        Updated = updated;
    }

    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Entidade do tenant.
    /// </summary>
    public virtual TenantEntity Tenant { get; set; }

    /// <summary>
    /// Entidade do tenant identity configuration.
    /// </summary>
    public virtual TenantIdentityConfigurationEntity TenantIdentityConfiguration { get; set; }

    /// <summary>
    /// Entidade do tenant email configuration.
    /// </summary>
    public virtual TenantEmailConfigurationEntity TenantEmailConfiguration { get; set; }

    /// <summary>
    /// Chave de acesso do tenant.
    /// </summary>
    public string TenantKey { get; set; }
}
