namespace AUTHIO.DOMAIN.Contracts;

/// <summary>
/// Interface de HangFire Jobs provider.
/// </summary>
public interface IHangFireJobsProvider
{
    /// <summary>
    /// Registra os Jobs do Hangfire.
    /// </summary>
    void RegisterJobs();
}
