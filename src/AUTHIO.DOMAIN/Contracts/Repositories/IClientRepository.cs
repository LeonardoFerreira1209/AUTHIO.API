using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Clients.
/// </summary>
public interface IClientRepository
    : IGenerictEntityCoreRepository<ClientEntity>
{
    /// <summary>
    /// Vincula um usuário admin e um Client.
    /// </summary>
    /// <param name="ClientId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task LinkClientWithUserAdminAsync(Guid ClientId, Guid userId);

    /// <summary>
    /// Verifica se um Client existe baseado na Client key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<bool> ExistsByKey(string key);
}
