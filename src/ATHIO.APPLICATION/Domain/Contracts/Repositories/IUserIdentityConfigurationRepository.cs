using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories;

/// <summary>
/// Repositorio de User identity configurations.
/// </summary>
public interface IUserIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<UserIdentityConfigurationEntity>
{ }
