using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Tenant token configurations.
/// </summary>
public sealed class TenantTokenConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantTokenConfigurationEntity>(context), ITenantTokenConfigurationRepository
{ }
