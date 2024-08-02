using AUTHIO.DOMAIN.Contracts;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Hangfire;
using Microsoft.Extensions.Options;
using Serilog;

namespace AUTHIO.INFRASTRUCTURE.Jobs.Hangfire;

/// <summary>
/// Provider de Hangfire jobs.
/// </summary>
/// <param name="recurringJobManager"></param>
public class HangfireJobsProvider(
    IRecurringJobManager recurringJobManager, IOptions<AppSettings> configurations) : IHangFireJobsProvider
{
    /// <summary>
    /// Registra os jobs.
    /// </summary>
    public void RegisterJobs()
    {
        try
        {
            Log.Information($"[LOG INFORMATION] - Inicializando os Jobs do Hangfire.\n");

            if (configurations.Value.Hangfire.ExecuteSendEventsToBusJob)
            {
                Log.Information($"[LOG INFORMATION] - Job SendEventsToBusAsync iniciado com sucesso.\n");

                recurringJobManager.AddOrUpdate<IEventService>(
                    "SendEventsToBus", x => x.SendEventsToBusAsync(), Cron.Hourly());
            }
        }
        catch(Exception exception) 
        {
            Log.Error($"[LOG ERRO] - Falha na inicialização dos Jobs do Hangfire. ({exception.Message})\n");
        }
    }
}
