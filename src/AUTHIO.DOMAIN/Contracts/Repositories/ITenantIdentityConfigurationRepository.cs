using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Interface de Repositorio de Tenant identity configurations.
/// </summary>
public interface ITenantIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<TenantIdentityConfigurationEntity>
{ }
