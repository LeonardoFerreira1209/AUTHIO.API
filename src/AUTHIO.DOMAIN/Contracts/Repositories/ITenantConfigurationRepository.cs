using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Tenant configurations.
/// </summary>
public interface ITenantConfigurationRepository
    : IGenerictEntityCoreRepository<TenantConfigurationEntity>
{ }
