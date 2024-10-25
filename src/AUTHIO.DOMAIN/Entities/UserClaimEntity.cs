using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de claims de usuários.
/// </summary>
public class UserClaimEntity : IdentityUserClaim<Guid>
{
    public string CustomProperty { get; set; }
}
