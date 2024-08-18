using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Password identity configurations.
/// </summary>
public sealed class PasswordIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<PasswordIdentityConfigurationEntity>(context), IPasswordIdentityConfigurationRepository
{ }
