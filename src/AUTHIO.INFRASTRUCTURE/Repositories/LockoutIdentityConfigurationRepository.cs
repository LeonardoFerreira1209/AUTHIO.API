using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Lockout identity configurations.
/// </summary>
public sealed class LockoutIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<LockoutIdentityConfigurationEntity>(context), ILockoutIdentityConfigurationRepository
{ }
