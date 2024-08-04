using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.BASE;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Tenant configurations.
/// </summary>
public sealed class TenantConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantConfigurationEntity>(context), ITenantConfigurationRepository
{ }
