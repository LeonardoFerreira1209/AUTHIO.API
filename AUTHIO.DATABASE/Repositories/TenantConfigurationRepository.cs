using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de Tenant configurations.
/// </summary>
public sealed class TenantConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantConfigurationEntity>(context), ITenantConfigurationRepository
{ }
