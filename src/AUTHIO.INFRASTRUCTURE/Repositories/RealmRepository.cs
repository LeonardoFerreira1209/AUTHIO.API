using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Realms.
/// </summary>
public sealed class RealmRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<RealmEntity>(context), IRealmRepository
{

}
