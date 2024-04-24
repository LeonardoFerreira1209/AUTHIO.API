using AUTHIO.DATBASE.Context;
using AUTHIO.DATBASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATBASE.Repositories;

/// <summary>
/// Repositorio de Password identity configurations.
/// </summary>
public sealed class PasswordIdentityConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<PasswordIdentityConfigurationEntity>(context), IPasswordIdentityConfigurationRepository
{ }
