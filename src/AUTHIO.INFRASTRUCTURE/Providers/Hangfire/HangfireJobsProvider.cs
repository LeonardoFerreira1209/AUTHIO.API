using AUTHIO.DOMAIN.Contracts;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static AUTHIO.DOMAIN.Dtos.Configurations.Hangfire;

namespace AUTHIO.INFRASTRUCTURE.Providers.Hangfire;

/// <summary>
/// Provider de Hangfire jobs.
/// </summary>
/// <param name="recurringJobManager"></param>
public class HangfireJobsProvider(
    IRecurringJobManager recurringJobManager,
    IOptions<AppSettings> configurations,
    ITaskJobFactory taskJobFactory) : IHangFireJobsProvider
{
    /// <summary>
    /// Registra os jobs.
    /// </summary>
    public void RegisterJobs()
    {
        try
        {
            Log.Information($"[LOG INFORMATION] - Inicializando os Jobs do Hangfire.\n");

            List<JobInfo> jobs
                = configurations.Value.Hangfire.Jobs;

            jobs.ToList().ForEach(job =>
            {
                AddOrRemoveJob(
                    job.Name,
                    job.Cronn.IsNullOrEmpty() ? Cron.Monthly() : job.Cronn,
                    job.Execute
                );
            });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERRO] - Falha na inicialização dos Jobs do Hangfire. ({exception.Message})\n");
        }
    }

    /// <summary>
    /// Adiciona, Atualiza ou Remove um job.
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="cronn"></param>
    /// <param name="execute"></param>
    private void AddOrRemoveJob(
        string jobName, string cronn, bool execute)
    {
        if (!execute)
        {
            recurringJobManager.RemoveIfExists(jobName);

            Log.Information($"[LOG INFORMATION] - Job {jobName} desativado com sucesso!\n");

            return;
        }

        var jobTask = taskJobFactory.GetJobTask(jobName);

        recurringJobManager.AddOrUpdate(
           jobName, Job.FromExpression(() => jobTask.ExecuteAsync()), cronn);

        Log.Information($"[LOG INFORMATION] - Job {jobName} ativado com sucesso!\n");
    }
}
