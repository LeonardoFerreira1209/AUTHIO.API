namespace AUTHIO.DOMAIN.Contracts.Jobs;

/// <summary>
/// Interface de execução de jobs.
/// </summary>
public interface IJob
{
    /// <summary>
    /// Executa uma task do Job.
    /// </summary>
    /// <returns></returns>
    Task ExecuteAsync();
}
