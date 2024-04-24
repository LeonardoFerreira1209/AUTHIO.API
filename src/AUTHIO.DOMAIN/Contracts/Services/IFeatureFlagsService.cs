namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de contexto de http.
/// </summary>
public interface IFeatureFlagsService
{
    /// <summary>
    /// Verifica se endponint está cadastro, caso esteja verifica se esta ativo, se não cadastra.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodName"></param>
    /// <param name="method"></param>
    /// <param name="methodDescription"></param>
    /// <returns></returns>
    Task<T> ExecuteAsync<T>(string methodName, Func<Task<T>> method, string methodDescription);
}
