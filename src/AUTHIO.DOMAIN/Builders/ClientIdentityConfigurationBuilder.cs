using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de ClientIdentityConfiguration.
/// </summary>
public sealed class ClientIdentityConfigurationBuilder
{
    /// <summary>
    /// Private properties.
    /// </summary>
    private Guid ClientConfigurationId;
    private DateTime? updated = null;
    private DateTime created;
    private UserIdentityConfigurationEntity userIdentityConfiguration;
    private PasswordIdentityConfigurationEntity passwordIdentityConfiguration;
    private LockoutIdentityConfigurationEntity lockoutIdentityConfiguration;

    /// <summary>
    /// Adiciona um Client configuration Id.
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <returns></returns>
    public ClientIdentityConfigurationBuilder AddClientConfigurationId(Guid ClientConfigurationId)
    {
        this.ClientConfigurationId = ClientConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public ClientIdentityConfigurationBuilder AddCreated(DateTime? created = null)
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
    public ClientIdentityConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona Identity user configuration.
    /// </summary>
    /// <param name="userIdentityConfiguration"></param>
    /// <returns></returns>
    public ClientIdentityConfigurationBuilder AddUserIdentityConfiguration(UserIdentityConfigurationEntity userIdentityConfiguration)
    {
        this.userIdentityConfiguration = userIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Identity password configuration.
    /// </summary>
    /// <param name="passwordIdentityConfiguration"></param>
    /// <returns></returns>
    public ClientIdentityConfigurationBuilder AddPasswordIdentityConfiguration(PasswordIdentityConfigurationEntity passwordIdentityConfiguration)
    {
        this.passwordIdentityConfiguration = passwordIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Identity lockout configuration.
    /// </summary>
    /// <param name="lockoutIdentityConfiguration"></param>
    /// <returns></returns>
    public ClientIdentityConfigurationBuilder AddLockoutIdentityConfiguration(LockoutIdentityConfigurationEntity lockoutIdentityConfiguration)
    {
        this.lockoutIdentityConfiguration = lockoutIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public ClientIdentityConfigurationEntity Builder()
        => new(ClientConfigurationId, 
            created, 
            updated,
            userIdentityConfiguration,
            passwordIdentityConfiguration,
            lockoutIdentityConfiguration
        );
}
