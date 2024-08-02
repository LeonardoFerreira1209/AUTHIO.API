using AUTHIO.DOMAIN.Contracts.Services;
using Microsoft.Extensions.Caching.Memory;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de controle de cache do sistema.
/// </summary>
/// <param name="memoryCache"></param>
public class CachingService(
    IMemoryCache memoryCache) : ICachingService
{
    /// <summary>
    /// Busca ou cria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="expiration"></param>
    /// <returns></returns>
    public async Task<T> GetOrCreateAsync<T>(
        string key, 
        Func<Task<T>> factory, 
        TimeSpan? expiration = null) where T : class
    {
        if (!memoryCache.TryGetValue(key, out T cacheEntry)) { 

            cacheEntry = await factory();
            await SetAsync(
                key, 
                cacheEntry, 
                expiration
            );
        }

        return cacheEntry;
    }

    /// <summary>
    /// Cria um cache.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="key"></param>
    /// <param name="request"></param>
    /// <param name="slidingExpiration"></param>
    /// <returns></returns>
    public async Task SetAsync<TRequest>(
        string key, 
        TRequest request, 
        TimeSpan? expiration = null) where TRequest : class
    {
        memoryCache.Set(
            key, 
            request,
            expiration ?? TimeSpan.FromMinutes(10)
        );

        await Task.CompletedTask;
    }

    /// <summary>
    /// Busca o cache.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<TResponse> GetAsync<TResponse>(
        string key) where TResponse : class
    {
        return await Task.FromResult(
            memoryCache.TryGetValue(
                key, 
                out TResponse value
            ) ? value : default
        );
    }
}
