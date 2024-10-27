using AUTHIO.DOMAIN.Auth.Token;
using AUTHIO.DOMAIN.Entities;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AUTHIO.DOMAIN.Builders.Token;

/// <summary>
/// Classe responsável por construir um token jwt.
/// </summary>
public class TokenJwtBuilder
{
    /// <summary>
    /// Chave de segurança.
    /// </summary>
    private SecurityKey securityKey = null;

    /// <summary>
    /// Variaveis 
    /// </summary>
    private string subject, issuer, audience, username;

    /// <summary>
    /// Claims
    /// </summary>
    private readonly List<Claim> claims
        = []; private readonly List<Claim> roles = [];

    /// <summary>
    /// Tempo de expiração do token.
    /// </summary>
    private int expiryInMinutes = 10;

    /// <summary>
    /// Tempo de expiração do refresh token.
    /// </summary>
    private int expiryRefreshTokenInHours = 24;

    /// <summary>
    /// Deve encriptar o token.
    /// </summary>
    private bool encrypted = false;

    /// <summary>
    /// Método que adiciona o username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddUsername(string username)
    {
        this.username = username;

        return this;
    }

    /// <summary>
    /// Método que adiciona o securotyKey.
    /// </summary>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddSecurityKey(SecurityKey securityKey)
    {
        this.securityKey = securityKey;

        return this;
    }

    /// <summary>
    /// Método que adiciona o subject.
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddSubject(string subject)
    {
        this.subject = subject;

        return this;
    }

    /// <summary>
    /// Método que adiciona o issuer.
    /// </summary>
    /// <param name="issuer"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddIssuer(string issuer)
    {
        this.issuer = issuer;

        return this;
    }

    /// <summary>
    /// Método que adiciona o audience.
    /// </summary>
    /// <param name="audience"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddAudience(string audience)
    {
        this.audience = audience;

        return this;
    }

    /// <summary>
    /// Método que adicona uma role.
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddRole(Claim role)
    {
        roles.Add(role);

        return this;
    }

    /// <summary>
    /// Método que adiciona várias roles.
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddRoles(List<Claim> roles)
    {
        this.roles.AddRange(roles);

        return this;
    }

    /// <summary>
    /// Método que adiciona uma claim.
    /// </summary>
    /// <param name="claim"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddClaim(Claim claim)
    {
        claims.Add(claim);

        return this;
    }

    /// <summary>
    /// Método que adiciona várias claims.
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddClaims(List<Claim> claims)
    {
        this.claims.AddRange(claims);

        return this;
    }

    /// <summary>
    /// Método que adiciona o tempo de expiração.
    /// </summary>
    /// <param name="expiryInMinutes"></param>
    /// <returns></returns>
    public TokenJwtBuilder AddExpiry(int expiryInMinutes)
    {
        this.expiryInMinutes = expiryInMinutes;

        return this;
    }

    /// <summary>
    ///  Método que adiciona a flag se deve ou não encriptar o token.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public TokenJwtBuilder IsEncrypyted(bool encrypted)
    {
        this.encrypted = encrypted;

        return this;
    }

    /// <summary>
    /// Método que cria e retorna o token.
    /// </summary>
    /// <returns></returns>
    public TokenJWT Builder(
        UserEntity userEntity,
        bool encrypted,
        EncryptingCredentials encryptedToken = null,
        SigningCredentials key = null)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TokenJwtBuilder)} - METHOD {nameof(Builder)}\n");

        try
        {
            var baseClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Name, userEntity.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, userEntity.LastName),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now).ToString(CultureInfo.CurrentCulture), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Typ, "Bearer"),
                new Claim(JwtRegisteredClaimNames.Email, userEntity.Email),
                new Claim(JwtRegisteredClaimNames.Website, "authioapi-production.up.railway.app"),

            }.Union(roles).Union(claims);

            Log.Information($"[LOG INFORMATION] - Token gerado com sucesso.\n");

            SecurityTokenDescriptor token = new()
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(baseClaims),
                Expires = DateTime.Now.AddMinutes(expiryInMinutes),
            };

            SecurityTokenDescriptor refreshToken = new()
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddHours(expiryRefreshTokenInHours),
            };

            (token, refreshToken) = SetCredentials(
                token,
                refreshToken,
                encrypted,
                encryptedToken,
                key
            );

            return new TokenJWT(
                token,
                refreshToken
            );
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}\n"); throw;
        }
    }

    /// <summary>
    /// Seta as credencials do token, encriptada ou não.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="refreshToken"></param>
    /// <param name="encrypted"></param>
    /// <param name="encryptedToken"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private (SecurityTokenDescriptor token, SecurityTokenDescriptor refreshToken) SetCredentials(
        SecurityTokenDescriptor token,
        SecurityTokenDescriptor refreshToken,
        bool encrypted,
        EncryptingCredentials encryptedToken = null,
        SigningCredentials key = null)
    {
        if (encrypted)
        {
            token.EncryptingCredentials = encryptedToken;
            refreshToken.EncryptingCredentials = encryptedToken;

            return (token, refreshToken);
        }

        token.SigningCredentials = key;
        refreshToken.SigningCredentials = key;

        return (token, refreshToken);
    }
}

