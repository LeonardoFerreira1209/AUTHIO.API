using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

public interface IOpenConnectService
{
    /// <summary>
    /// Recupera as configurações do Open Id.
    /// </summary>
    /// <param name="validate"></param>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    Task<ObjectResult> GetOpenIdConnectConfigurationAsync(
        string tenantKey = null
    );

    /// <summary>
    ///  Recupera as chaves de segurança para autenticação.
    /// </summary>
    /// <param name="validate"></param>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    Task<object> GetJwksAsync(
        string tenantKey = null
    );
}
