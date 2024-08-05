namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de Execução de task jobs.
/// </summary>
public interface IExecuteJobTask
{
    /// <summary>
    /// Executa uma task.
    /// </summary>
    /// <returns></returns>
    Task ExecuteAsync();
}
