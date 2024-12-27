﻿using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de Open Id.
/// </summary>
/// <param name="tenantRepository"></param>
/// <param name="jwtService"></param>
/// <param name="contextService"></param>
public class OpenConnectService(
    ITenantRepository tenantRepository,
    IJwtService jwtService,
    IContextService contextService) : IOpenConnectService
{
    /// <summary>
    /// Recupera as configurações do Open Id.
    /// </summary>
    /// <returns></returns>
    public async Task<ObjectResult> GetOpenIdConnectConfigurationAsync(
            string tenantKey = null
        )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(OpenConnectService)} - METHOD {nameof(GetOpenIdConnectConfigurationAsync)}\n");

        try
        {
            OpenIdConnectConfiguration openIdConnectConfiguration = new() {
                AuthorizationEndpoint = $"{contextService.GetUrlBase()}/api/authentications//signin",
                JwksUri = $"{contextService.GetUrlBase()}/jwks"
            };

            if (tenantKey is not null)
            {
                var exists = await tenantRepository
                   .ExistsByKey(
                       tenantKey
                   );

                if (!exists)
                    throw new Exception(
                        "Tenant não existe, verifica se a key esta correta!"
                    );

                var tenant = await tenantRepository
                    .GetAsync(
                        tenant => tenant
                            .TenantConfiguration
                                .TenantKey == tenantKey
                    );

                var tenantTokenConfiguration = tenant
                    .TenantConfiguration
                    .TenantTokenConfiguration;

                openIdConnectConfiguration = new OpenIdConnectConfiguration {
                    Issuer = tenantTokenConfiguration.Issuer,
                    AuthorizationEndpoint = $"{contextService.GetUrlBase()}/api/authentications/tenants/{tenantKey}/signin",
                    JwksUri = $"{contextService.GetUrlBase()}/tenants/{tenantKey}/jwks"
                };
            }

            ObjectResponse response = new(
                HttpStatusCode.OK,
                new ApiResponse<OpenIdConnectConfiguration>(
                    true, HttpStatusCode.OK, 
                    openIdConnectConfiguration, [
                        new("Configurações retornadas com sucesso!")
                    ]
                )
            );

            return response;
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
        string tenantKey = null
        )
    {
        Log.Information(
           $"[LOG INFORMATION] - SET TITLE {nameof(OpenConnectService)} - METHOD {nameof(GetJwksAsync)}\n");

        try
        {
            if(tenantKey is not null)
            {
                var exists = await tenantRepository
                 .ExistsByKey(
                     tenantKey
                 );

                if (!exists)
                    throw new Exception(
                        "Tenant não existe, verifica se a key esta correta!"
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
