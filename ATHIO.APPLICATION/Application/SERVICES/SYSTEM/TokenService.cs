using AUTHIO.APPLICATION.Domain.Dtos.Configurations;
using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.DOMAIN.BUILDERS.TOKEN;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.TOKEN;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.APPLICATION.SERVICES.SYSTEM;

/// <summary>
/// Token service
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="userManager"></param>
/// <param name="roleManager"></param>
/// <param name="appsettings"></param>
public class TokenService(UserManager<UserEntity> userManager, 
    RoleManager<RoleEntity> roleManager, IOptions<AppSettings> appsettings) : ITokenService
{
    private readonly UserManager<UserEntity> 
        _userManager = userManager;

    private readonly RoleManager<RoleEntity> 
        _roleManager = roleManager;

    private readonly IOptions<AppSettings> 
        _appsettings = appsettings;

    /// <summary>
    /// Cria o JWT TOKEN
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundUserException"></exception>
    public async Task<TokenJWT> CreateJsonWebToken(string username)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TokenService)} - METHOD {nameof(CreateJsonWebToken)}\n");

        return await
           BuildTokenJWT(username);
    }

    /// <summary>
    /// Cria o JWT TOKEN através de um REFRESH TOKEN.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundUserException"></exception>
    public async Task<TokenJWT> CreateJsonWebTokenByRefreshToken(string refreshToken)
    {
        Log.Information($"[LOG INFORMATION] - SET TITLE {nameof(TokenService)} - METHOD {nameof(CreateJsonWebTokenByRefreshToken)}\n");

        var tokenValidationResult = await
            new JwtSecurityTokenHandler().ValidateTokenAsync(refreshToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = JwtSecurityKey.Create(_appsettings.Value.Auth.SecurityKey),
                    ValidateIssuer = true,
                    ValidIssuer = _appsettings.Value.Auth.ValidIssuer,
                    ValidateAudience = true,
                    ValidAudience = _appsettings.Value.Auth.ValidAudience,
                    ClockSkew = TimeSpan.FromHours(3),
                });

        var username = tokenValidationResult.IsValid
            ? (string)tokenValidationResult.Claims.First().Value : throw new TokenJwtException(tokenValidationResult);

        return await
            BuildTokenJWT(username);
    }

    /// <summary>
    /// Constói o tokenJwt.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<TokenJWT> BuildTokenJWT(string username)
    {
        var userEntity = await User(username) ?? throw new NotFoundUserException(username);

        var roles = await Roles(userEntity);

        var claims = await Claims(userEntity, roles);

        Log.Information($"[LOG INFORMATION] - Criando o token do usuário.\n");

        return await Task.FromResult(
            new TokenJwtBuilder()
              .AddUsername(username)
                .AddSecurityKey(JwtSecurityKey.Create(_appsettings.Value.Auth.SecurityKey))
                   .AddSubject("HYPER.IO PROJECTS L.T.D.A")
                      .AddIssuer(_appsettings.Value.Auth.ValidIssuer)
                          .AddAudience(_appsettings.Value.Auth.ValidAudience)
                              .AddExpiry(_appsettings.Value.Auth.ExpiresIn)
                                  .AddRoles(roles)
                                      .AddClaims(claims)
                                          .Builder(userEntity));
    }

    /// <summary>
    /// Return de User.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<UserEntity> User(string username)
        => await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));

    /// <summary>
    /// Return de Roles.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<List<Claim>> Roles(UserEntity user)
    {
        return await _userManager.GetRolesAsync(user).ContinueWith(rolesTask =>
        {
            var roles = rolesTask.Result;

            return roles.Select(roleName => new Claim("role", roleName)).ToList();
        });
    }

    /// <summary>
    /// Return de Claims.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<List<Claim>> Claims(UserEntity user, List<Claim> roles)
    {
        var claims = new List<Claim>();

        claims.AddRange(await _userManager.GetClaimsAsync(user));

        var rolesName = roles.Select(role => role.Value).ToList();

        if (roles is not null && roles.Count != 0)
        {
            await _roleManager.Roles.Where(role
                => rolesName.Contains(role.Name)).ToListAsync().ContinueWith(rolesTask =>
                {
                    var roles = rolesTask.Result;

                    roles.AsParallel().ForAll(role
                        => claims.AddRange(_roleManager.GetClaimsAsync(role).Result));
                });
        }

        return claims.Distinct().ToList();
    }
}
