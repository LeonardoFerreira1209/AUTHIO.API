using AUTHIO.DATBASE.Context;
using AUTHIO.DATBASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATBASE.Repositories;

/// <summary>
/// Repositorio de Lockout identity configurations.
/// </summary>
public sealed class LockoutIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<LockoutIdentityConfigurationEntity>(context), ILockoutIdentityConfigurationRepository
{ }
