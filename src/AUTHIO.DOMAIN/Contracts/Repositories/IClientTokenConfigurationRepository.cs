using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Client token configurations.
/// </summary>
public interface IClientTokenConfigurationRepository
    : IGenerictEntityCoreRepository<ClientTokenConfigurationEntity>
{ }
