using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.INFRASTRUCTURE.Factories;

/// <summary>
/// Factory de Task de jobs.
/// </summary>
/// <param name="serviceProvider"></param>
public class TaskJobFactory(
    IServiceProvider serviceProvider) : ITaskJobFactory
{
    /// <summary>
    /// Recupera uma instancia do IExecuteJobTask baseada no nome.
    /// </summary>
    /// <param name="jobName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public IJob GetJobTask(string jobName) => jobName switch {
        "SendEventsToBus" 
            => serviceProvider.GetService<ISendEventToBusJob>(),
        "SyncStripeProducts"
            => serviceProvider.GetService<IStripeSyncProductsJob>(),
        _ => 
            throw new ArgumentException("Job não existe", nameof(jobName))
    };
}
