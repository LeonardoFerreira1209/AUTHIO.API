using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Dtos.Model;
using System.Collections.ObjectModel;

namespace AUTHIO.INFRASTRUCTURE.Repositories.Store;

/// <summary>
/// Armazenamento de chaves em memoria.
/// </summary>
public class InMemoryStore : IJsonWebKeyStore
{
    public const string DefaultRevocationReason = "Revoked";
    private static readonly List<KeyMaterial> _store = [];
    private readonly SemaphoreSlim _slim = new(1);

    /// <summary>
    /// Armazenar.
    /// </summary>
    /// <param name="keyMaterial"></param>
    /// <returns></returns>
    public Task Store(KeyMaterial keyMaterial)
    {
        _slim.Wait();
        _store.Add(keyMaterial);
        _slim.Release();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Buscar atual.
    /// </summary>
    /// <returns></returns>
    public Task<KeyMaterial> GetCurrent()
        => Task.FromResult(_store.OrderByDescending(s => s.CreationDate).FirstOrDefault());

    /// <summary>
    /// Revogar.
    /// </summary>
    /// <param name="keyMaterial"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    public async Task Revoke(KeyMaterial keyMaterial, string reason = null)
    {
        if (keyMaterial == null)
            return;

        var revokeReason = reason ?? DefaultRevocationReason;

        keyMaterial.Revoke(revokeReason);

        var oldOne = _store.Find(f => f.Id == keyMaterial.Id);

        if (oldOne != null)
        {
            var index = _store.FindIndex(f => f.Id == keyMaterial.Id);
            await _slim.WaitAsync();
            _store.RemoveAt(index);
            _store.Insert(index, keyMaterial);
            _slim.Release();
        }
    }

    /// <summary>
    /// Buscar ultimas chaves.
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int quantity)
    {
        return Task.FromResult(
            _store
                .OrderByDescending(s => s.CreationDate)
                .Take(quantity).ToList().AsReadOnly());
    }

    /// <summary>
    /// Recuperar.
    /// </summary>
    /// <param name="keyId"></param>
    /// <returns></returns>
    public Task<KeyMaterial> Get(string keyId)
        => Task.FromResult(_store.FirstOrDefault(w => w.KeyId == keyId));

    /// <summary>
    /// Limpar.
    /// </summary>
    /// <returns></returns>
    public Task Clear()
    {
        _store.Clear();

        return Task.CompletedTask;
    }
}