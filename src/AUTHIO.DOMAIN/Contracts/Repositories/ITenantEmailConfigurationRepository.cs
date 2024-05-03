using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Tenant email configurations.
/// </summary>
public interface ITenantEmailConfigurationRepository
    : IGenerictEntityCoreRepository<TenantEmailConfigurationEntity>
{ }
