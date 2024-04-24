using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Exceptions.Base;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.API.Controllers.Base;

/// <summary>
/// Controller base
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="featureFlags"></param>
/// <param name=""></param>
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
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    protected async Task<T> ExecuteAsync<T>(string methodName,
        Func<Task<T>> method, string methodDescription) =>
            await _featureFlags.ExecuteAsync(methodName, method, methodDescription);
}
