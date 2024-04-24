using AUTHIO.DATBASE.Context;
using AUTHIO.DATBASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATBASE.Repositories;

/// <summary>
/// Repositório de Tenant Identity Configuration Reopository.
/// </summary>
/// <param name="context"></param>
public sealed class TenantIdentityConfigurationRepository(AuthIoContext context)
    : GenericEntityCoreRepository<TenantIdentityConfigurationEntity>(context), ITenantIdentityConfigurationRepository
{

}
