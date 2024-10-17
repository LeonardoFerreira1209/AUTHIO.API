using AUTHIO.DOMAIN.Helpers.Jwa;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

/// <summary>
/// Interface de CryptoService.
/// </summary>
public interface ICryptoService
{
    /// <summary>
    /// Criar chave de segurança.
    /// </summary>
    /// <param name="keySize"></param>
    /// <returns></returns>
    RsaSecurityKey CreateRsaSecurityKey(int keySize = 3072);

    /// <summary>
    /// Criar um id unico.
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    string CreateUniqueId(int length = 16);

    /// <summary>
    /// Criar chave de segurança ECDsa.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    ECDsaSecurityKey CreateECDsaSecurityKey(Algorithm algorithm);

    /// <summary>
    /// Recuperar um CurveType.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    string GetCurveType(Algorithm algorithm);

    /// <summary>
    /// Criar chave de segurança Hmac.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    HMAC CreateHmacSecurityKey(Algorithm algorithm);

    /// <summary>
    /// Criar chave de segurança AESS.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    Aes CreateAESSecurityKey(Algorithm algorithm);

    /// <summary>
    /// Recuperar um ECCURVE nomeado.
    /// </summary>
    /// <param name="curveId"></param>
    /// <returns></returns>
    ECCurve GetNamedECCurve(string curveId);

    /// <summary>
    /// Criar uma chave random key.
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    byte[] CreateRandomKey(int length);
}
