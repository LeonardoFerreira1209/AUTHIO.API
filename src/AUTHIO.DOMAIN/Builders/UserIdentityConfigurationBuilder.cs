using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de User identity configurations.
/// </summary>
public sealed class UserIdentityConfigurationBuilder
{
    private Guid ClientIdentityConfigurationId;
    private DateTime? updated = null;
    private DateTime created;
    private string allowedUserNameCharacters;
    private bool requireUniqueEmail;

    /// <summary>
    /// Adiciona uma allowedUserNameCharacters.
    /// </summary>
    /// <param name="allowedUserNameCharacters"></param>
    /// <returns></returns>
    public UserIdentityConfigurationBuilder AddAllowedUserNameCharacters(string allowedUserNameCharacters)
    {
        this.allowedUserNameCharacters = allowedUserNameCharacters;

        return this;
    }

    /// <summary>
    /// Adiciona uma RequireUniqueEmail.
    /// </summary>
    /// <param name="requireUniqueEmail"></param>
    /// <returns></returns>
    public UserIdentityConfigurationBuilder AddRequireUniqueEmail(bool requireUniqueEmail)
    {
        this.requireUniqueEmail = requireUniqueEmail;

        return this;
    }

    /// <summary>
    /// Adiciona um ClientIdentityConfigurationId.
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <returns></returns>
    public UserIdentityConfigurationBuilder AddClientIdentityConfigurationId(Guid ClientIdentityConfigurationId)
    {
        this.ClientIdentityConfigurationId = ClientIdentityConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public UserIdentityConfigurationBuilder AddCreated(DateTime? created = null)
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
    public UserIdentityConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public UserIdentityConfigurationEntity Builder()
        => new(ClientIdentityConfigurationId, created,
            updated, requireUniqueEmail, allowedUserNameCharacters);
}
