using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Builders;

/// <summary>
/// Classe de builder de TenantConfiguration.
/// </summary>
public class TenantConfigurationBuilder
{
    private string tenantKey; 
    private Guid tenantId;
    private DateTime? updated = null;
    private DateTime created;

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
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantConfigurationEntity Builder() 
        => new(tenantKey, tenantId, created, updated);
}
