using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Infra.Context;
using AUTHIO.APPLICATION.Infra.Repositories.BASE;

namespace AUTHIO.APPLICATION.Infra.Repositories;

/// <summary>
/// Repositorio de Lockout identity configurations.
/// </summary>
public sealed class LockoutIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<LockoutIdentityConfigurationEntity>(context), ILockoutIdentityConfigurationRepository
{ }
