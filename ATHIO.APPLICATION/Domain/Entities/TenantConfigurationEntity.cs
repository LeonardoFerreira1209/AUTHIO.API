using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// Classe de configuração de Tenanty.
/// </summary>
public class TenantConfigurationEntity : IEntityBase
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantConfigurationEntity() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="apikey"></param>
    /// <param name="tenantId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="status"></param>
    public TenantConfigurationEntity(
        string apikey, Guid tenantId, 
        DateTime created, DateTime? updated, Status status)
    {
        ApiKey = apikey;
        TenantId = tenantId;
        Created = created;
        Updated = updated;
        Status = status;
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
    /// Status.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Chave de acesso.
    /// </summary>
    public string ApiKey { get; set; }
}
