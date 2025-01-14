using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação do Realm.
/// </summary>
public static class CreateRealm
{
    /// <summary>
    /// Cria um Realm com os dados de cadastro inicial.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static RealmEntity CreateDefault(
        Guid userId,
        string name, 
        string description
        )
        => new RealmBuilder()
            .AddUserId(userId)
            .AddName(name)
            .AddDescription(description)
            .AddStatus(Status.Ativo)
            .AddCreated()
            .Builder();
}
