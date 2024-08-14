using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AUTHIO.DOMAIN.Auth;

/// <summary>
/// Handler de validação de token.
/// </summary>
/// <param name="serviceProvider"></param>
public class JwtServiceValidationHandler(
    IServiceProvider serviceProvider) : JwtSecurityTokenHandler
{
    /// <summary>
    /// Método de validação de token.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="validationParameters"></param>
    /// <param name="validatedToken"></param>
    /// <returns></returns>
    public override ClaimsPrincipal ValidateToken(string token,
        TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        using var scope = serviceProvider.CreateScope();
        var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
        var contextService = scope.ServiceProvider.GetRequiredService<IContextService>();
        var configurations = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();

        var keyMaterialTask = jwtService.GetLastKeys();
        keyMaterialTask.Wait();

        if (contextService.TryGetValueByHeader(
                      "Authorization", out StringValues authHeader))
        {
            var tenantKey =
                GetTenantKeyByToken(authHeader.ToString());

            if (tenantKey is not null)
            {
                ITenantConfigurationRepository tenantConfigurationRepository =
                    scope.ServiceProvider
                        .GetRequiredService<ITenantConfigurationRepository>();

                var tenantConfigurationEntity =
                    tenantConfigurationRepository
                        .GetAsync(x => x.TenantKey == tenantKey).Result;

                var tenantTokenConfiguration =
                    tenantConfigurationEntity.TenantTokenConfiguration;

                validationParameters = new TokenValidationParameters
                {
                    LogValidationExceptions = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromHours(3),

                    ValidIssuer = tenantTokenConfiguration.Issuer,
                    ValidAudience = tenantTokenConfiguration.Audience,
                    IssuerSigningKeys = keyMaterialTask.Result.Select(s => s.GetSecurityKey())
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tenantTokenConfiguration.SecurityKey))
                };
            }
        }
       
        validationParameters = new TokenValidationParameters
        {
            LogValidationExceptions = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromHours(3),

            ValidIssuer = configurations.Value.Auth.ValidIssuer,
            ValidAudience = configurations.Value.Auth.ValidAudience,
            IssuerSigningKeys = keyMaterialTask.Result.Select(s => s.GetSecurityKey())
            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.Value.Auth.SecurityKey))
        };

        //We can read the token before we've begun validating it.
        JwtSecurityToken incomingToken = ReadJwtToken(token);

        //And let the framework take it from here.
        //var handler = new JsonWebTokenHandler();
        //var result = handler.ValidateToken(token, validationParameters);
        //validatedToken = result.SecurityToken;

        //return new ClaimsPrincipal(result.ClaimsIdentity);
        return base.ValidateToken(token, validationParameters, out validatedToken);
    }

    /// <summary>
    /// Recupera um tenantKey do JwtToken.
    /// </summary>
    /// <param name="authHeader"></param>
    /// <returns></returns>
    private static string GetTenantKeyByToken(string authHeader)
    {
        var handler =
            new JwtSecurityTokenHandler();

        string tokenWithoutBearer
            = authHeader.ToString()
                .Replace("Bearer ", "");

        var tokenJson
            = handler.ReadToken(
                tokenWithoutBearer) as JwtSecurityToken;

        return tokenJson.Claims
            .FirstOrDefault(x =>
                x.Type == "tenantKey")?.Value;
    }
}