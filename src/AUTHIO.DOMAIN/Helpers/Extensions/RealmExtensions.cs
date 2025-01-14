using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de extensão de Realms.
/// </summary>
public static class RealmExtensions
{
    /// <summary>
    ///  Transforma created request para entity.
    /// </summary>
    /// <param name="createRealmRequest"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static RealmEntity ToEntity(this CreateRealmRequest createRealmRequest, Guid userId)
        => CreateRealm.CreateDefault(
            userId,
            createRealmRequest.Name,
            createRealmRequest.Description
        );

    /// <summary>
    /// Atualiza a entidade de Realm.
    /// </summary>
    /// <param name="updateRealmRequest"></param>
    /// <param name="realmEntity"></param>
    /// <returns></returns>
    public static RealmEntity UpdateEntity(
        this UpdateRealmRequest updateRealmRequest, 
        RealmEntity realmEntity
        )
    {
        realmEntity.Name = updateRealmRequest.Name;
        realmEntity.Description = updateRealmRequest.Description;
        realmEntity.Updated = DateTime.Now;

        return realmEntity;
    }

    /// <summary>
    /// Transforma um Realm Entity em response.
    /// </summary>
    /// <param name="realmEntity"></param>
    /// <param name="includeClients"></param>
    /// <returns></returns>
    public static RealmResponse ToResponse(this RealmEntity realmEntity,
        bool includeClients = true)
        => new()
        {
            Id = realmEntity.Id,
            Created = realmEntity.Created,
            Updated = realmEntity.Updated,
            Name = realmEntity.Name,
            Description = realmEntity.Description,
            Status = realmEntity.Status,

            Clients = includeClients ? realmEntity
                .Clients?.Select(
                    client => client.ToResponse()

                ).ToList() : []
        };
}
