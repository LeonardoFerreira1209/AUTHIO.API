using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Builder de Client.
/// </summary>
public sealed class ClientBuilder
{
    /// <summary>
    /// private properties
    /// </summary>
    private Guid userId;
    private DateTime created;
    private DateTime? updated = null;
    private Status status;
    private string name;
    private string description;
    private ClientConfigurationEntity ClientConfiguration;

    /// <summary>
    /// Adiciona o id do usuario.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public ClientBuilder AddUserId(Guid userId)
    {
        this.userId = userId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public ClientBuilder AddCreated(DateTime? created = null)
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
    public ClientBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona o status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public ClientBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ClientBuilder AddName(string name)
    {
        this.name = name;

        return this;
    }

    /// <summary>
    /// Adiciona a descrição.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public ClientBuilder AddDescription(string description)
    {
        this.description = description;

        return this;
    }

    /// <summary>
    /// Add Client Configuration.
    /// </summary>
    /// <param name="ClientConfiguration"></param>
    /// <returns></returns>
    public ClientBuilder AddClientConfiguration(ClientConfigurationEntity ClientConfiguration)
    {
        this.ClientConfiguration = ClientConfiguration;

        return this;
    }

    /// <summary>
    /// Builder
    /// </summary>
    /// <returns></returns>
    public ClientEntity Builder() =>
        new(userId,
            created, updated,
            status, name,
            description, ClientConfiguration
        );
}
