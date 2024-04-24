using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositório de Tenant Identity Configuration Reopository.
/// </summary>
/// <param name="context"></param>
public sealed class TenantIdentityConfigurationRepository(AuthIoContext context)
    : GenericEntityCoreRepository<TenantIdentityConfigurationEntity>(context), ITenantIdentityConfigurationRepository
{

}
