using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de User identity configurations.
/// </summary>
public sealed class UserIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<UserIdentityConfigurationEntity>(
            context), IUserIdentityConfigurationRepository
{ }
