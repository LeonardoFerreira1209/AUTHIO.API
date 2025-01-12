using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Interface de Repositorio de Client identity configurations.
/// </summary>
public interface IClientIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<ClientIdentityConfigurationEntity>
{ }
