using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Password identity configurations.
/// </summary>
public interface IPasswordIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<PasswordIdentityConfigurationEntity>
{ }
