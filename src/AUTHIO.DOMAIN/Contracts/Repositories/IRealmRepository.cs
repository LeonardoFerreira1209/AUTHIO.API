using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Realms.
/// </summary>
public interface IRealmRepository
    : IGenerictEntityCoreRepository<RealmEntity>
{

}
