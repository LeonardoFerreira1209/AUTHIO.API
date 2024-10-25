using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de user token.
/// </summary>
public class UserTokenEntity : IdentityUserToken<Guid>
{
    public string CustomProperty { get; set; }
}
