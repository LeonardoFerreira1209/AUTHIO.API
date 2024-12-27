using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de logins de usuários.
/// </summary>
public class UserLoginEntity : IdentityUserLogin<Guid>
{
    public string CustomProperty { get; set; }
}
