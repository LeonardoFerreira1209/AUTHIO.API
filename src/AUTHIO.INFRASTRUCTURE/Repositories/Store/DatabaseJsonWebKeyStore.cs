using AUTHIO.DOMAIN.Constants;
using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Model;
using AUTHIO.DOMAIN.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Serilog;
using System.Collections.ObjectModel;

namespace AUTHIO.INFRASTRUCTURE.Repositories.Store;

/// <summary>
/// Armazenamento de chaves na Base.
/// </summary>
/// <typeparam name="TContext"></typeparam>
/// <param name="context"></param>
/// <param name="options"></param>
/// <param name="memoryCache"></param>
public class DataBaseJsonWebKeyStore<TContext>(
    TContext context,
    IOptions<JwtOptions> options,
    IMemoryCache memoryCache,
    IContextService contextService,
    ITenantRepository tenantRepository) : IJsonWebKeyStore where TContext : DbContext, ISecurityKeyContext
{
    private readonly Guid? _currentTenantId 
        = tenantRepository.GetAsync(
            x => x.TenantConfiguration.TenantKey == contextService.GetCurrentTenantKey())?.Result?.Id;

    /// <summary>
    /// Armazenar.
    /// </summary>
    /// <param name="securityParamteres"></param>
    /// <returns></returns>
    public async Task Store(KeyMaterial securityParamteres)
    {
        await context.SecurityKeys.AddAsync(securityParamteres);

        Log.Information($"Saving new SecurityKeyWithPrivate {securityParamteres.Id}", typeof(TContext).Name);

        await context.SaveChangesAsync();

        ClearCache();
    }

    /// <summary>
    /// Recuperar atual.
    /// </summary>
    /// <returns></returns>
    public async Task<KeyMaterial> GetCurrent()
    {
        string cacheKey =
            $"{JwkContants.CurrentJwkCache}{_currentTenantId?.ToString()}";

        if (!memoryCache.TryGetValue(
            cacheKey, 
            out KeyMaterial credentials))
        {
            IQueryable<KeyMaterial> query 
                = context.SecurityKeys;

            if(_currentTenantId.HasValue)
                query = query.Where(
                    x => x.TenantId == _currentTenantId);

            credentials = await query.Where(keyMa => !keyMa.IsRevoked)
                    .OrderByDescending(d => d.CreationDate)
                        .AsNoTrackingWithIdentityResolution()
                            .FirstOrDefaultAsync();

            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(options.Value.CacheTime);

            if (credentials != null)
                memoryCache.Set(cacheKey, credentials, cacheEntryOptions);

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
        string cacheKey =
            $"{JwkContants.JwksCache}{_currentTenantId?.ToString()}";

        if (!memoryCache.TryGetValue(
            cacheKey, out ReadOnlyCollection<KeyMaterial> keys))
        {

            keys = context.SecurityKeys.OrderByDescending(
                d => d.CreationDate).Take(quantity)
                    .AsNoTrackingWithIdentityResolution().ToList().AsReadOnly();

            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(options.Value.CacheTime);

            if (keys.Count != 0)
                memoryCache.Set(cacheKey, keys, cacheEntryOptions);

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
        => context.SecurityKeys.FirstOrDefaultAsync(f => f.KeyId == keyId);

    /// <summary>
    /// Limpar.
    /// </summary>
    /// <returns></returns>
    public async Task Clear()
    {
        foreach (var securityKeyWithPrivate in context.SecurityKeys)
            context.SecurityKeys.Remove(securityKeyWithPrivate);

        await context.SaveChangesAsync();

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

        context.Attach(securityKeyWithPrivate);

        context.SecurityKeys.Update(securityKeyWithPrivate);

        await context.SaveChangesAsync();

        ClearCache();
    }

    /// <summary>
    /// Limpar cache.
    /// </summary>
    private void ClearCache()
    {
        string cacheKey =
            $"{JwkContants.JwksCache}{_currentTenantId?.ToString()}";

        string currentCacheKey =
           $"{JwkContants.CurrentJwkCache}{_currentTenantId?.ToString()}";

        memoryCache.Remove(cacheKey);

        memoryCache.Remove(currentCacheKey);
    }
}
