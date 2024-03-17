using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// Classe de configuração de Tenanty.
/// </summary>
public class TenantConfigEntity :
    IEntityBase, IEntityTenant
{
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
    /// Status.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Id do tenanty responsável.
    /// </summary>
    public Guid TenantId { get; set; }
}
