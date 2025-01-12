using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Client email configurations.
/// </summary>
public interface IClientEmailConfigurationRepository
    : IGenerictEntityCoreRepository<ClientEmailConfigurationEntity>
{ }
