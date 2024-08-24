using AUTHIO.DOMAIN.Dtos.Model;
using System.Collections.ObjectModel;

namespace AUTHIO.DOMAIN.Contracts.Jwt;

/// <summary>
/// Interface de Store de chaves.
/// </summary>
public interface IJsonWebKeyStore
{
    /// <summary>
    /// Armazenar.
    /// </summary>
    /// <param name="keyMaterial"></param>
    /// <returns></returns>
    Task Store(KeyMaterial keyMaterial);

    /// <summary>
    /// Buscar atual.
    /// </summary>
    /// <returns></returns>
    Task<KeyMaterial> GetCurrent();

    /// <summary>
    /// Revogar.
    /// </summary>
    /// <param name="keyMaterial"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    Task Revoke(KeyMaterial keyMaterial, string reason = default);

    /// <summary>
    /// Buscar ultima chave.
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int quantity);

    /// <summary>
    /// Buscar.
    /// </summary>
    /// <param name="keyId"></param>
    /// <returns></returns>
    Task<KeyMaterial> Get(string keyId);

    /// <summary>
    /// Limpar.
    /// </summary>
    /// <returns></returns>
    Task Clear();
}