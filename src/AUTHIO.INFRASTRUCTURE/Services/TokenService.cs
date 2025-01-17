﻿using AUTHIO.DOMAIN.Auth.Token;
using AUTHIO.DOMAIN.Builders.Token;
using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Expressions.Filters;
using AUTHIO.INFRASTRUCTURE.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Token service
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="userManager"></param>
/// <param name="roleManager"></param>
/// <param name="appsettings"></param>
public class TokenService(
    CustomUserManager<UserEntity> userManager,
    RoleManager<RoleEntity> roleManager,
    IContextService contextService,
    IJwtService jwtService,
    IOptions<AppSettings> appsettings,
    ITenantTokenConfigurationRepository tenantTokenConfigurationRepository) : ITokenService
{
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
           BuildToken(username);
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
                    IssuerSigningKey = JwtSecurityKey.Create(appsettings.Value.Auth.SecurityKey),
                    ValidateIssuer = true,
                    ValidIssuer = appsettings.Value.Auth.ValidIssuer,
                    ValidateAudience = true,
                    ValidAudience = appsettings.Value.Auth.ValidAudience,
                    ClockSkew = TimeSpan.Zero,
                }
            );

        var username = tokenValidationResult.IsValid
            ? (string)tokenValidationResult.Claims.First().Value : throw new TokenJwtException(tokenValidationResult);

        return await
            BuildToken(username);
    }

    /// <summary>
    /// Constói o tokenJwt.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<TokenJWT> BuildToken(string username)
    {
        var userEntity = await userManager.FindByNameWithExpressionAsync(username, 
            UserFilters<UserEntity>.FilterSystemOrTenantUsers(
                contextService.GetCurrentTenantKey())) ?? throw new NotFoundUserException(username);

        var roles = await Roles(userEntity);

        var claims = await Claims(userEntity, roles);

        if (userEntity?.Tenant?.TenantConfiguration is not null)
            claims.Add(new Claim("x-tenant-key", userEntity.Tenant.TenantConfiguration.TenantKey));

        Log.Information($"[LOG INFORMATION] - Criando o token do usuário.\n");

        var tenantTokenConfiguration = await tenantTokenConfigurationRepository
            .GetAsync(ttc => !userEntity.System && 
                ttc.TenantConfigurationId == userEntity.Tenant.TenantConfiguration.Id);

        var tokenConfigs = new {

            SecurityKey = tenantTokenConfiguration?.SecurityKey 
                ?? appsettings.Value.Auth.SecurityKey,
            ValidIssuer = tenantTokenConfiguration?.Issuer 
                ?? appsettings.Value.Auth.ValidIssuer,
            ValidAudience = tenantTokenConfiguration?.Audience
                ?? appsettings.Value.Auth.ValidAudience
        };

        bool encrypted = tenantTokenConfiguration?.Encrypted ?? false;

        SigningCredentials key = 
            await jwtService.GetCurrentSigningCredentials();

        EncryptingCredentials encryptitedKey =
          await jwtService.GetCurrentEncryptingCredentials();

        return await Task.FromResult(
            new TokenJwtBuilder()
              .AddUsername(username)
                .AddSecurityKey(JwtSecurityKey.Create(tokenConfigs.SecurityKey))
                   .AddSubject(userEntity.Id.ToString())
                      .AddIssuer(tokenConfigs.ValidIssuer)
                          .AddAudience(tokenConfigs.ValidAudience)
                              .AddExpiry(appsettings.Value.Auth.ExpiresIn)
                                  .AddRoles(roles)
                                      .AddClaims(claims)
                                        .IsEncrypyted(tenantTokenConfiguration?.Encrypted ?? false)
                                            .Builder(userEntity, encrypted, encryptitedKey, key)
        );
    }

    /// <summary>
    /// Return de Roles.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<List<Claim>> Roles(UserEntity user)
    {
        return await userManager.GetRolesAsync(user).ContinueWith(rolesTask =>
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

        claims.AddRange(await userManager.GetClaimsAsync(user));

        var rolesName = roles.Select(role => role.Value).ToList();

        if (roles is not null && roles.Count != 0)
        {
            await roleManager.Roles.Where(role
                => rolesName.Contains(role.Name)).ToListAsync().ContinueWith(rolesTask =>
                {
                    var roles = rolesTask.Result;

                    roles.AsParallel().ForAll(role
                        => claims.AddRange(roleManager.GetClaimsAsync(role).Result));
                });
        }

        return claims.Distinct().ToList();
    }
}
