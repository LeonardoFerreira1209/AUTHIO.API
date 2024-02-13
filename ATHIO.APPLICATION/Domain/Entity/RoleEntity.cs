using AUTHIO.APPLICATION.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// Classe de entidade de roles.
/// </summary>
public class RoleEntity : IdentityRole<Guid>, IEntityBase, IEntityTenant
{
    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; } = null;

    /// <summary>
    /// Status do cadastro.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public virtual TenantEntity Tenant { get; private set; }

    /// <summary>
    /// Role do sistema.
    /// </summary>
    public bool System { get; set; }
}
