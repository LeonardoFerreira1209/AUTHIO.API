using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories;

/// <summary>
/// Repositorio de Tenant configurations.
/// </summary>
public interface ITenantConfigurationRepository
    : IGenerictEntityCoreRepository<TenantConfigurationEntity>
{ }
