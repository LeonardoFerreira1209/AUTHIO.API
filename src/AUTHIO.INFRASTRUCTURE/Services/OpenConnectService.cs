using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using Serilog;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de Open Id.
/// </summary>
/// <param name="ClientRepository"></param>
/// <param name="jwtService"></param>
/// <param name="contextService"></param>
public class OpenConnectService(
    IOptions<AppSettings> options,
    IClientRepository ClientRepository,
    IJwtService jwtService,
    IContextService contextService) : IOpenConnectService
{
    /// <summary>
    /// Recupera as configurações do Open Id.
    /// </summary>
    /// <returns></returns>
    public async Task<OpenIdConnectConfiguration> GetOpenIdConnectConfigurationAsync(
            string ClientKey = null
        )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(OpenConnectService)} - METHOD {nameof(GetOpenIdConnectConfigurationAsync)}\n");

        try
        {
            OpenIdConnectConfiguration openIdConnectConfiguration = new() {
                AuthorizationEndpoint = $"{contextService.GetUrlBase()}/api/authentications/signin",
                JwksUri = $"{contextService.GetUrlBase()}/jwks",
                TokenEndpoint = $"{contextService.GetUrlBase()}/token",
                FrontchannelLogoutSessionSupported = false.ToString(),
                FrontchannelLogoutSupported = false.ToString(),
                Issuer = options.Value.Auth.ValidIssuer
            };

            if (ClientKey is not null)
            {
                var exists = await ClientRepository
                   .ExistsByKey(
                       ClientKey
                   );

                if (!exists)
                    throw new Exception(
                        "Client não existe, verifica se a key esta correta!"
                    );

                var Client = await ClientRepository
                    .GetAsync(
                        Client => Client
                            .ClientConfiguration
                                .ClientKey == ClientKey
                    );

                var ClientTokenConfiguration = Client
                    .ClientConfiguration
                    .ClientTokenConfiguration;

                openIdConnectConfiguration = new OpenIdConnectConfiguration {
                    AuthorizationEndpoint = $"{contextService.GetUrlBase()}/api/authentications/Clients/{ClientKey}/signin",
                    JwksUri = $"{contextService.GetUrlBase()}/Clients/{ClientKey}/jwks",
                    TokenEndpoint = $"{contextService.GetUrlBase()}/Clients/{ClientKey}/token",
                    FrontchannelLogoutSessionSupported = false.ToString(),
                    FrontchannelLogoutSupported = false.ToString(),
                    Issuer = ClientTokenConfiguration.Issuer
                };
            }

            return openIdConnectConfiguration;
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception:{exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    /// Recupera as chaves de segurança para autenticação.
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetJwksAsync(
        string ClientKey = null
        )
    {
        Log.Information(
           $"[LOG INFORMATION] - SET TITLE {nameof(OpenConnectService)} - METHOD {nameof(GetJwksAsync)}\n");

        try
        {
            if(ClientKey is not null)
            {
                var exists = await ClientRepository
                 .ExistsByKey(
                     ClientKey
                 );

                if (!exists)
                    throw new Exception(
                        "Client não existe, verifica se a key esta correta!"
                    );
            }

            var storedKeys 
                = await jwtService
                    .GetLastKeys();

            var keys = new
            {
                keys = storedKeys.Select(
                    s => s.GetSecurityKey()
                )
            };

            return keys;
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception:{exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
