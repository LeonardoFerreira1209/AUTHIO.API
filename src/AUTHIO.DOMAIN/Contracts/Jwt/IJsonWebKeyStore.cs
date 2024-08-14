using AUTHIO.DOMAIN.Dtos.Model;
using System.Collections.ObjectModel;

namespace AUTHIO.DOMAIN.Contracts.Jwt;

public interface IJsonWebKeyStore
{
    Task Store(KeyMaterial keyMaterial);
    Task<KeyMaterial> GetCurrent();
    Task Revoke(KeyMaterial keyMaterial, string reason = default);
    Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int quantity);
    Task<KeyMaterial> Get(string keyId);
    Task Clear();
}