using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de Tenant email configurations.
/// </summary>
public sealed class TenantEmailConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantEmailConfigurationEntity>(context), ITenantEmailConfigurationRepository
{ }
