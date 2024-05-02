using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AUTHIO.DOMAIN.Auth.Token;

/// <summary>
/// Classe de chave de segurança do Jwt.
/// </summary>
public static class JwtSecurityKey
{
    /// <summary>
    /// Cria uma chave de segurança simetrica.
    /// </summary>
    /// <param name="secret"></param>
    /// <returns></returns>
    public static SymmetricSecurityKey Create(
        string secret) => new(Encoding.ASCII.GetBytes(secret));
}
