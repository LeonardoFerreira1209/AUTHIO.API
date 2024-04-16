using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.TOKEN;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AUTHIO.APPLICATION.DOMAIN.BUILDERS.TOKEN;

/// <summary>
/// Classe responsável por construir um token jwt.
/// </summary>
public class TokenJwtBuilder
{
    private SecurityKey securityKey = null;

    private string subject, issuer, audience, username;

    private readonly List<Claim> claims = []; private readonly List<Claim> roles = [];

    private int expiryInMinutes = 10;
    private readonly int expiryRefreshTokenInHours = 24;

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
    /// Método que cria e retorna o token.
    /// </summary>
    /// <returns></returns>
    public TokenJWT Builder(UserEntity userEntity)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TokenJwtBuilder)} - METHOD {nameof(Builder)}\n");

        try
        {
            var baseClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(DateTime.Now.AddHours(-3)).ToString(CultureInfo.CurrentCulture), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Typ, "Bearer"),
                new Claim(JwtRegisteredClaimNames.Email, userEntity.Email),
                new Claim(ClaimTypes.MobilePhone, userEntity.PhoneNumber ?? string.Empty),
                new Claim(ClaimTypes.Webpage, "https://www.linkedin.com/in/leonardoferreiraalmeida/"),

            }.Union(roles).Union(claims);

            Log.Information($"[LOG INFORMATION] - Token gerado com sucesso.\n");

            return new TokenJWT(
                            new JwtSecurityToken(
                                issuer: issuer,
                                audience: audience,
                                claims: baseClaims,
                                expires: DateTime.UtcNow.AddHours(-3).AddMinutes(expiryInMinutes),
                                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)),

                            new JwtSecurityToken(
                                issuer: issuer,
                                audience: audience,
                                claims: new[] { new Claim(JwtRegisteredClaimNames.UniqueName, username) },
                                expires: DateTime.UtcNow.AddHours(expiryRefreshTokenInHours).AddHours(-3),
                                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256))
                        );
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - {exception.Message}\n"); throw;
        }
    }
}

