using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de Lockout identity configurations.
/// </summary>
public sealed class LockoutIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<LockoutIdentityConfigurationEntity>(context), ILockoutIdentityConfigurationRepository
{ }
