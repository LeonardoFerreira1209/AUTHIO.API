using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Builders;

/// <summary>
/// Classe de builder de TenantConfiguration.
/// </summary>
public class TenantConfigurationBuilder
{
    private string apikey; 
    private Guid tenantId;
    private DateTime? updated = null;
    private DateTime created;
    private Status status;

    /// <summary>
    /// Adiciona uma apikey.
    /// </summary>
    /// <param name="apikey"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddApikey(string apikey)
    {
        this.apikey = apikey;

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
    /// Adiciona o status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public TenantConfigurationBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantConfigurationEntity Builder() 
        => new(apikey, tenantId, created, updated, status);
}
