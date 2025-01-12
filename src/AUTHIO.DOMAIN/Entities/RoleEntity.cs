using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade de roles.
/// </summary>
public class RoleEntity : IdentityRole<Guid>,
    IEntityBase, IEntityClientNullAble, IEntitySystem
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
    /// Id do Client.
    /// </summary>
    public Guid? ClientId { get; set; }

    /// <summary>
    /// Client.
    /// </summary>
    public virtual ClientEntity Client { get; private set; }

    /// <summary>
    /// Role do sistema.
    /// </summary>
    public bool System { get; set; }
}
