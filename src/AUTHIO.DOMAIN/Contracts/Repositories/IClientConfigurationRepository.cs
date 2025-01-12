using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Client configurations.
/// </summary>
public interface IClientConfigurationRepository
    : IGenerictEntityCoreRepository<ClientConfigurationEntity>
{ }
