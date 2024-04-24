using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de User identity configurations.
/// </summary>
public interface IUserIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<UserIdentityConfigurationEntity>
{ }
