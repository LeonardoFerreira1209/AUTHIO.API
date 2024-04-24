using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de User identity configurations.
/// </summary>
public sealed class UserIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<UserIdentityConfigurationEntity>(context), IUserIdentityConfigurationRepository
{ }
