using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.BASE;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositório de Tenant Identity Configuration Reopository.
/// </summary>
/// <param name="context"></param>
public sealed class TenantIdentityConfigurationRepository(AuthIoContext context)
    : GenericEntityCoreRepository<TenantIdentityConfigurationEntity>(context), ITenantIdentityConfigurationRepository
{

}
