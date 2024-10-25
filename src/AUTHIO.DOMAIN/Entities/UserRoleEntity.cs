using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de roles de usuários.
/// </summary>
public class UserRoleEntity : IdentityUserRole<Guid>
{
    /// <summary>
    /// Gets or sets the primary key of the user that is linked to a role.
    /// </summary>
    public override Guid UserId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the primary key of the role that is linked to the user.
    /// </summary>
    public override Guid RoleId { get; set; } = default!;
}
