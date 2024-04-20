using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.TOKEN;

public static class JwtSecurityKey
{
    public static SymmetricSecurityKey Create(string secret) => new(Encoding.ASCII.GetBytes(secret));
}
