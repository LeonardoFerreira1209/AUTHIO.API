using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace AUTHIO.DOMAIN.Auth.Token;

/// <summary>
/// Classe referente ao Token
/// </summary>
public class TokenJWT
{
    /// <summary>
    /// Token de segurança
    /// </summary>
    private readonly SecurityTokenDescriptor token;

    /// <summary>
    /// Token de refresh.
    /// </summary>
    private readonly SecurityTokenDescriptor refreshToken;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="token"></param>
    internal TokenJWT(SecurityTokenDescriptor token, SecurityTokenDescriptor refreshToken)
    {
        this.token = token;
        this.refreshToken = refreshToken;
    }

    /// <summary>
    /// Validade do token
    /// </summary>
    public DateTime ValidTo => token.ValidTo;

    /// <summary>
    /// Valor do token.
    /// </summary>
    public string Token => new JsonWebTokenHandler().CreateToken(token);

    /// <summary>
    /// Token de refresh.
    /// </summary>
    public string RefreshToken => new JsonWebTokenHandler().CreateToken(refreshToken);
}

