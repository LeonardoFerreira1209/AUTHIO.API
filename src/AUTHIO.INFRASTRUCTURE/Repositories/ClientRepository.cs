using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Clients.
/// </summary>
public sealed class ClientRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<ClientEntity>(context), IClientRepository
{
    private readonly AuthIoContext _context = context;

    /// <summary>
    /// Vincula um usuário admin e um Client.
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task LinkClientWithUserAdminAsync(Guid clientId, Guid userId)
        => await _context.AddAsync(
            new ClientIdentityUserAdminEntity { ClientId = clientId, UserId = userId });

    /// <summary>
    /// Verifica se um Client existe baseado na Client key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<bool> ExistsByKey(string key)
    {
        var exists = await _context
            .Clients.AnyAsync(
                x => x.ClientConfiguration
                    .ClientKey.Equals(key) 
                        && x.Status == Status.Ativo
            );

        return exists;
    }
}
