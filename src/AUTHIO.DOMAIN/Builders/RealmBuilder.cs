using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Builder de Realm.
/// </summary>
public sealed class RealmBuilder
{
    /// <summary>
    /// private properties
    /// </summary>
    private Guid userId;
    private Guid realmId;
    private DateTime created;
    private DateTime? updated = null;
    private Status status;
    private string name;
    private string description;

    /// <summary>
    /// Adiciona o id do usuario.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public RealmBuilder AddUserId(Guid userId)
    {
        this.userId = userId;

        return this;
    }

    /// <summary>
    /// Adiciona o id do realm.
    /// </summary>
    /// <param name="realmId"></param>
    /// <returns></returns>
    public RealmBuilder AddRealmId(Guid realmId)
    {
        this.realmId = realmId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public RealmBuilder AddCreated(DateTime? created = null)
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
    public RealmBuilder AddUpdated(DateTime? updated = null)
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
    public RealmBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public RealmBuilder AddName(string name)
    {
        this.name = name;

        return this;
    }

    /// <summary>
    /// Adiciona a descrição.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public RealmBuilder AddDescription(string description)
    {
        this.description = description;

        return this;
    }

    /// <summary>
    /// Builder
    /// </summary>
    /// <returns></returns>
    public RealmEntity Builder() =>
        new(userId, realmId,
            created, updated,
            status, name,
            description
        );
}
