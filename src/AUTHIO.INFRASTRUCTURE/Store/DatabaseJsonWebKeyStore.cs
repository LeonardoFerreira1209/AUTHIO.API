using AUTHIO.DOMAIN.Constants;
using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Model;
using AUTHIO.DOMAIN.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

namespace AUTHIO.INFRASTRUCTURE.Store;

/// <summary>
/// Armazenamento de chaves na base.
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <param name="context"></param>
/// <param name="logger"></param>
/// <param name="options"></param>
/// <param name="memoryCache"></param>
public class DatabaseJsonWebKeyStore<TContext>(
    TContext context,
    ILogger<DatabaseJsonWebKeyStore<TContext>> logger,
    IOptions<JwtOptions> options,
    IMemoryCache memoryCache) : IJsonWebKeyStore where TContext : DbContext, ISecurityKeyContext
{
    /// <summary>
    /// Contexto.
    /// </summary>
    private readonly TContext _context = context;

    /// <summary>
    /// Opções de Jwt.
    /// </summary>
    private readonly IOptions<JwtOptions> _options = options;

    /// <summary>
    /// Cache de memoria.
    /// </summary>
    private readonly IMemoryCache _memoryCache = memoryCache;

    /// <summary>
    /// Logs.
    /// </summary>
    private readonly ILogger<DatabaseJsonWebKeyStore<TContext>> _logger = logger;

    /// <summary>
    /// Armazenar.
    /// </summary>
    /// <param name="securityParamteres"></param>
    /// <returns></returns>
    public async Task Store(KeyMaterial securityParamteres)
    {
        await _context.SecurityKeys.AddAsync(securityParamteres);

        _logger.LogInformation($"Saving new SecurityKeyWithPrivate {securityParamteres.Id}", typeof(TContext).Name);

        await _context.SaveChangesAsync();

        ClearCache();
    }

    /// <summary>
    /// Recuperar atual.
    /// </summary>
    /// <returns></returns>
    public async Task<KeyMaterial> GetCurrent()
    {
        if (!_memoryCache.TryGetValue(JwkContants.CurrentJwkCache, out KeyMaterial credentials))
        {
            credentials = await _context.SecurityKeys.Where(
                X => X.IsRevoked == false)
                    .OrderByDescending(d => d.CreationDate)
                        .AsNoTrackingWithIdentityResolution()
                        .FirstOrDefaultAsync();

            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(_options.Value.CacheTime);

            if (credentials != null)
                _memoryCache.Set(JwkContants.CurrentJwkCache, credentials, cacheEntryOptions);

            return credentials;
        }

        return credentials;
    }

    /// <summary>
    /// Ultimas chaves.
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public async Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int quantity = 5)
    {
        if (!_memoryCache.TryGetValue(
            JwkContants.JwksCache, out ReadOnlyCollection<KeyMaterial> keys)) {

            keys = _context.SecurityKeys.OrderByDescending(
                d => d.CreationDate).Take(quantity)
                    .AsNoTrackingWithIdentityResolution().ToList().AsReadOnly();

            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(_options.Value.CacheTime);

            if (keys.Count != 0)
                _memoryCache.Set(JwkContants.JwksCache, keys, cacheEntryOptions);

           return await Task.FromResult(keys);
        }

        return await Task.FromResult(keys); ;
    }

    /// <summary>
    /// Recuperar.
    /// </summary>
    /// <param name="keyId"></param>
    /// <returns></returns>
    public Task<KeyMaterial> Get(string keyId) 
        => _context.SecurityKeys.FirstOrDefaultAsync(f => f.KeyId == keyId);

    /// <summary>
    /// Limpar.
    /// </summary>
    /// <returns></returns>
    public async Task Clear()
    {
        foreach (var securityKeyWithPrivate in _context.SecurityKeys)
            _context.SecurityKeys.Remove(securityKeyWithPrivate);

        await _context.SaveChangesAsync();

        ClearCache();
    }

    /// <summary>
    /// Revogar chaves.
    /// </summary>
    /// <param name="securityKeyWithPrivate"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    public async Task Revoke(
        KeyMaterial securityKeyWithPrivate, string reason = null)
    {
        if (securityKeyWithPrivate == null)
            return;

        securityKeyWithPrivate.Revoke(reason);

        _context.Attach(securityKeyWithPrivate);

        _context.SecurityKeys.Update(securityKeyWithPrivate);

        await _context.SaveChangesAsync();

        ClearCache();
    }

    /// <summary>
    /// Limpar cache.
    /// </summary>
    private void ClearCache()
    {
        _memoryCache.Remove(JwkContants.JwksCache);

        _memoryCache.Remove(JwkContants.CurrentJwkCache);
    }
}
