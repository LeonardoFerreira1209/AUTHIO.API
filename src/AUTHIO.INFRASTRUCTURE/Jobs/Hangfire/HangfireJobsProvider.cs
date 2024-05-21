using AUTHIO.DOMAIN.Contracts;
using AUTHIO.DOMAIN.Contracts.Services;
using Hangfire;
using Serilog;

namespace AUTHIO.INFRASTRUCTURE.Jobs.Hangfire;

/// <summary>
/// Provider de Hangfire jobs.
/// </summary>
/// <param name="recurringJobManager"></param>
public class HangfireJobsProvider(
    IRecurringJobManager recurringJobManager) : IHangFireJobsProvider
{
    /// <summary>
    /// Registra os jobs.
    /// </summary>
    public void RegisterJobs()
    {
        try
        {
            Log.Information($"[LOG INFORMATION] - Inicializando os Jobs do Hangfire.\n");

            recurringJobManager.AddOrUpdate<IEventService>(
                "SendEventsToBus", x => x.SendEventsToBusAsync(), Cron.Minutely());
        }
        catch(Exception exception) 
        {
            Log.Error($"[LOG ERRO] - Falha na inicialização dos Jobs do Hangfire. ({exception.Message})\n");
        }
    }
}
