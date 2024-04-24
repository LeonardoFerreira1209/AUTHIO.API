using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AUTHIO.DOMAIN.Auth.Token;

public static class JwtSecurityKey
{
    public static SymmetricSecurityKey Create(string secret) => new(Encoding.ASCII.GetBytes(secret));
}
