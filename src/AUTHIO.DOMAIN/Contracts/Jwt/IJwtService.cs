using AUTHIO.DOMAIN.Dtos.Model;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace AUTHIO.DOMAIN.Contracts.Jwt;

/// <summary>
/// Interface da service do JWT.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Gerar uma chave.
    /// </summary>
    /// <returns></returns>
    Task<SecurityKey> GenerateKey();

    /// <summary>
    /// Buscar chave de segurança atual.
    /// </summary>
    /// <returns></returns>
    Task<SecurityKey> GetCurrentSecurityKey();

    /// <summary>
    /// Buscar credencial de assinatura atual.
    /// </summary>
    /// <returns></returns>
    Task<SigningCredentials> GetCurrentSigningCredentials();

    /// <summary>
    /// Buscar credencial encripitada atual.
    /// </summary>
    /// <returns></returns>
    Task<EncryptingCredentials> GetCurrentEncryptingCredentials();

    /// <summary>
    /// Buscar ultima chave.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int? i = null);

    /// <summary>
    /// Revogar chave.
    /// </summary>
    /// <param name="keyId"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    Task RevokeKey(string keyId, string reason = null);

    /// <summary>
    /// Gerar nova chave.
    /// </summary>
    /// <returns></returns>
    Task<SecurityKey> GenerateNewKey();
}