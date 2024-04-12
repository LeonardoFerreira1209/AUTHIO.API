using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Builders;

/// <summary>
/// Classe de builder de TenantIdentityConfiguration.
/// </summary>
public class TenantIdentityConfigurationBuilder
{
    private Guid tenantConfigurationId;
    private DateTime? updated = null;
    private DateTime created;
    private Status status;

    /// <summary>
    /// Adiciona um TenantId.
    /// </summary>
    /// <param name="tenantId"></param>
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
    /// Adiciona o status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public TenantIdentityConfigurationBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantIdentityConfigurationEntity Builder() 
        => new(tenantConfigurationId, created, updated, status);
}
