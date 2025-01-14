using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de Realms.
/// </summary>
public interface IRealmService
{
    /// <summary>
    /// Método responsável por criar um Realm.
    /// </summary>
    /// <param name="createRealmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> CreateAsync(
        CreateRealmRequest createRealmRequest, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Método responsável por atualizar um Realm. 
    /// </summary>
    /// <param name="updateRealmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> UpdateAsync(
       UpdateRealmRequest updateRealmRequest, 
       CancellationToken cancellationToken
    );

    /// <summary>
    /// Recupera todos os Realms com paginação.
    /// </summary>
    /// <param name="filterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> GetAllAsync(
        FilterRequest filterRequest, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Método responsável por criar um client no Realm.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <param name="clientId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> RegisterRealmClientAsync(
        CreateClientRequest createClientRequest, 
        Guid realmId, 
        CancellationToken cancellationToken
    );
}
