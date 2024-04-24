using AUTHIO.DATBASE.Context;
using AUTHIO.DATBASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATBASE.Repositories;

/// <summary>
/// Repositorio de Tenant configurations.
/// </summary>
public sealed class TenantConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantConfigurationEntity>(context), ITenantConfigurationRepository
{ }
