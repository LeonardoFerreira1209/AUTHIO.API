namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

/// <summary>
/// Interface de serviço de cache.
/// </summary>
public interface ICachingService
{
    /// <summary>
    /// Busca ou cria cache.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="factory"></param>
    /// <param name="expiration"></param>
    /// <returns></returns>
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null) where T : class;

    /// <summary>
    /// Cria um cache.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="key"></param>
    /// <param name="request"></param>
    /// <param name="slidingExpiration"></param>
    /// <returns></returns>
    Task SetAsync<TRequest>(
        string key, TRequest request,
        TimeSpan? slidingExpiration = null) where TRequest : class;

    /// <summary>
    /// Busca o cache.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<TResponse> GetAsync<TResponse>(
        string key) where TResponse : class;
}
