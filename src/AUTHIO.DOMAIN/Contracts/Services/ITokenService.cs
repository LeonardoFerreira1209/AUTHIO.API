using AUTHIO.DOMAIN.Auth.Token;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Token service
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Criação do token.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<TokenJWT> CreateJsonWebToken(string username);

    /// <summary>
    /// Criação de token através de um refreshToken.
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <returns></returns>
    Task<TokenJWT> CreateJsonWebTokenByRefreshToken(string refreshToken);
}
