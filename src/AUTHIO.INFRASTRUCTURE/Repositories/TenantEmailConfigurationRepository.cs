using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Tenant email configurations.
/// </summary>
public sealed class TenantEmailConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantEmailConfigurationEntity>(context), ITenantEmailConfigurationRepository
{ }
