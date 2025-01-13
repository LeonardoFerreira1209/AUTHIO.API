using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

public interface IOpenConnectService
{
    /// <summary>
    /// Recupera as configurações do Open Id.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <returns></returns>
    Task<OpenIdConnectConfiguration> GetOpenIdConnectConfigurationAsync(
        string clientKey = null
    );

    /// <summary>
    ///  Recupera as chaves de segurança para autenticação.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <returns></returns>
    Task<object> GetJwksAsync(
        string clientKey = null
    );
}
