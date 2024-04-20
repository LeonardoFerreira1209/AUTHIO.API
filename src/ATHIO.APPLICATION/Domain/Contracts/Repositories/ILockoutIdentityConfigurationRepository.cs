using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories;

/// <summary>
/// Repositorio de Lockout identity configurations.
/// </summary>
public interface ILockoutIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<LockoutIdentityConfigurationEntity>
{ }
