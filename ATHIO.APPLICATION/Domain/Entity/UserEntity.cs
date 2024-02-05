using AUTHIO.APPLICATION.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// Classe de entidade de usuário.
/// </summary>
public class UserEntity : IdentityUser<Guid>, IEntityBase, IEntityTenant
{
    /// <summary>
    /// Id do tenant responsavel.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public virtual TenantEntity Tenant { get; private set; }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Ultimo nome do usuário.
    /// </summary>
    public string LastName { get; set; } = null;

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
}
