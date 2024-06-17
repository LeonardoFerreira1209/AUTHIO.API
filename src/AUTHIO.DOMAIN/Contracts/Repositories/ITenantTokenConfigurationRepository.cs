using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Tenant token configurations.
/// </summary>
public interface ITenantTokenConfigurationRepository
    : IGenerictEntityCoreRepository<TenantTokenConfigurationEntity>
{ }
