using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
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

        JwtSecurityToken incomingToken = new();

        var keyMaterialTask = 
            jwtService.GetLastKeys();

        keyMaterialTask.Wait();

        if (contextService.TryGetValueByHeader(
                "Authorization", out StringValues authHeader))
        {
            var clientKey =
                GetClientKeyByToken(authHeader.ToString()) 
                    ?? contextService.GetCurrentClientKey();

            if (clientKey is not null && contextService.IsAuthByClientKey)
            {
                IClientConfigurationRepository clientConfigurationRepository =
                    scope.ServiceProvider
                        .GetRequiredService<IClientConfigurationRepository>();

                var clientConfigurationEntity =
                    clientConfigurationRepository
                        .GetAsync(x => x.ClientKey == clientKey).Result;

                var clientTokenConfiguration =
                    clientConfigurationEntity.ClientTokenConfiguration;

                keyMaterialTask =
                    jwtService.GetLastKeys();

                keyMaterialTask.Wait();

                validationParameters = new TokenValidationParameters
                {
                    LogValidationExceptions = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = !clientTokenConfiguration.Encrypted,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = clientTokenConfiguration.Issuer,
                    ValidAudience = clientTokenConfiguration.Audience,
                    IssuerSigningKeys = keyMaterialTask.Result.Select(s => s.GetSecurityKey()),
                    TokenDecryptionKeys = keyMaterialTask.Result.Select(s => s.GetSecurityKey()),
                };

                incomingToken = ReadJwtToken(token);

                return base.ValidateToken(token, validationParameters, out validatedToken);
            }
        }
       
        validationParameters = new TokenValidationParameters
        {
            LogValidationExceptions = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireSignedTokens= true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configurations.Value.Auth.ValidIssuer,
            ValidAudience = configurations.Value.Auth.ValidAudience,
            IssuerSigningKeys = keyMaterialTask.Result.Select(s => s.GetSecurityKey())
        };

        incomingToken = ReadJwtToken(token);

        return base.ValidateToken(token, validationParameters, out validatedToken);
    }

    /// <summary>
    /// Recupera um ClientKey do JwtToken.
    /// </summary>
    /// <param name="authHeader"></param>
    /// <returns></returns>
    private static string GetClientKeyByToken(string authHeader)
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
                x.Type == "x-client-key")?.Value;
    }
}