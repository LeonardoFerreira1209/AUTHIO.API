using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories;

/// <summary>
/// Interface de Repositorio de Tenant identity configurations.
/// </summary>
public interface ITenantIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<TenantIdentityConfigurationEntity>
{ }
