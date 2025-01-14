using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de Clients.
/// </summary>
public interface IClientservice
{
    /// <summary>
    /// Método responsável por criar um Client.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <returns></returns>
    Task<ObjectResult> CreateAsync(
        CreateClientRequest createClientRequest, 
        Guid realmId,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Método responsável por atualizar um Client. 
    /// </summary>
    /// <param name="updateClientRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> UpdateAsync(
       UpdateClientRequest updateClientRequest, 
       CancellationToken cancellationToken
    );

    /// <summary>
    /// Recupera todos os Clients com paginação.
    /// </summary>
    /// <param name="filterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> GetAllAsync(FilterRequest filterRequest, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Recupera um Client pela chave.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> GetClientByKeyAsync(string clientKey, 
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Método responsável por criar um usuário no Client.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="clientKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> RegisterClientUserAsync(
        RegisterUserRequest registerUserRequest, 
        string clientKey, 
        CancellationToken cancellationToken
    );
}
