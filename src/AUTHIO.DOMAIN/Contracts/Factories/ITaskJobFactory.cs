using AUTHIO.DOMAIN.Contracts.Jobs;

namespace AUTHIO.DOMAIN.Contracts.Factories;

/// <summary>
/// Factory de funções executadas por jobs.
/// </summary>
public interface ITaskJobFactory
{
    /// <summary>
    /// Recupera uma instancia do IExecuteJobTask baseada no nome.
    /// </summary>
    /// <param name="jobName"></param>
    /// <returns></returns>
    IJob GetJobTask(string jobName);
}
