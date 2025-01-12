using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de LockoutIdentityConfiguration.
/// </summary>
public sealed class LockoutIdentityConfigurationBuilder
{
    private Guid ClientConfigurationId;
    private DateTime? updated = null;
    private DateTime created;
    private bool allowedForNewUsers;
    private int maxFailedAccessAttempts;
    private TimeSpan defaultLockoutTimeSpan;

    /// <summary>
    /// Adiciona um Client configuration Id.
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <returns></returns>
    public LockoutIdentityConfigurationBuilder AddClientConfigurationId(Guid ClientConfigurationId)
    {
        this.ClientConfigurationId = ClientConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public LockoutIdentityConfigurationBuilder AddCreated(DateTime? created = null)
    {
        this.created
            = created
            ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona a data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public LockoutIdentityConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona lock habilitado para novos usuários.
    /// </summary>
    /// <param name="allowedForNewUsers"></param>
    /// <returns></returns>
    public LockoutIdentityConfigurationBuilder AddAllowedForNewUsers(bool allowedForNewUsers)
    {
        this.allowedForNewUsers = allowedForNewUsers;

        return this;
    }

    /// <summary>
    /// Adiciona o número máximo de tentativas de acessos.
    /// </summary>
    /// <param name="maxFailedAccessAttempts"></param>
    /// <returns></returns>
    public LockoutIdentityConfigurationBuilder AddMaxFailedAccessAttempts(int maxFailedAccessAttempts)
    {
        this.maxFailedAccessAttempts = maxFailedAccessAttempts;

        return this;
    }

    /// <summary>
    /// Adiciona um tempo de lockout padrão.
    /// </summary>
    /// <param name="defaultLockoutTimeSpan"></param>
    /// <returns></returns>
    public LockoutIdentityConfigurationBuilder AddDefaultLockoutTimeSpan(TimeSpan defaultLockoutTimeSpan)
    {
        this.defaultLockoutTimeSpan = defaultLockoutTimeSpan;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public LockoutIdentityConfigurationEntity Builder()
        => new(ClientConfigurationId, created, updated,
            allowedForNewUsers, maxFailedAccessAttempts, defaultLockoutTimeSpan);
}
