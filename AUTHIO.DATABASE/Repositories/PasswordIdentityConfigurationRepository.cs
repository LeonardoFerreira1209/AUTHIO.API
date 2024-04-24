using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de Password identity configurations.
/// </summary>
public sealed class PasswordIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<PasswordIdentityConfigurationEntity>(context), IPasswordIdentityConfigurationRepository
{ }
