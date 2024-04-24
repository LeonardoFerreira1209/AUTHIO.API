using AUTHIO.DATBASE.Context;
using AUTHIO.DATBASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATBASE.Repositories;

/// <summary>
/// Repositorio de User identity configurations.
/// </summary>
public sealed class UserIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<UserIdentityConfigurationEntity>(context), IUserIdentityConfigurationRepository
{ }
