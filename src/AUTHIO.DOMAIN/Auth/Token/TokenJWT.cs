using System.IdentityModel.Tokens.Jwt;

namespace AUTHIO.DOMAIN.Auth.Token;

/// <summary>
/// Classe referente ao Token
/// </summary>
public class TokenJWT
{
    /// <summary>
    /// Token de segurança
    /// </summary>
    private readonly JwtSecurityToken token;

    /// <summary>
    /// Token de refresh.
    /// </summary>
    private readonly JwtSecurityToken refreshToken;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="token"></param>
    internal TokenJWT(JwtSecurityToken token, JwtSecurityToken refreshToken)
    {
        this.token = token;
        this.refreshToken = refreshToken;
    }

    /// <summary>
    /// Validade do token
    /// </summary>
    public DateTime ValidTo => token.ValidTo.AddHours(3);

    /// <summary>
    /// Valor do token.
    /// </summary>
    public string Token => new JwtSecurityTokenHandler().WriteToken(token);

    /// <summary>
    /// Token de refresh.
    /// </summary>
    public string RefreshToken => new JwtSecurityTokenHandler().WriteToken(refreshToken);
}

