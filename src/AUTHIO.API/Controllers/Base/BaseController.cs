using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.API.Controllers.Base;

/// <summary>
/// Controller base
/// </summary>
/// <param name="featureFlags"></param>
public class BaseController(
    IFeatureFlagsService featureFlags) : ControllerBase
{
    private readonly IFeatureFlagsService _featureFlags = featureFlags;

    /// <summary>
    /// Método que verifica se o endpoint está ativado.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodName"></param>
    /// <param name="method"></param>
    /// <param name="methodDescription"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async Task<T> ExecuteAsync<T>(
        string methodName,
        Func<Task<T>> method, 
        string methodDescription, 
        CancellationToken cancellationToken
        ) =>
            await _featureFlags
                .ExecuteAsync(
                    methodName, 
                    method, 
                    methodDescription, 
                    cancellationToken
                );
}
