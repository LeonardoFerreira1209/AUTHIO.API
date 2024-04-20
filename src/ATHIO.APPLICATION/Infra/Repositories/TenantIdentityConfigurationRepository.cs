using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Infra.Context;
using AUTHIO.APPLICATION.Infra.Repositories.BASE;

namespace AUTHIO.APPLICATION.Infra.Repositories;

/// <summary>
/// Repositório de Tenant Identity Configuration Reopository.
/// </summary>
/// <param name="context"></param>
public sealed class TenantIdentityConfigurationRepository(AuthIoContext context) 
    : GenericEntityCoreRepository<TenantIdentityConfigurationEntity>(context), ITenantIdentityConfigurationRepository
{

}
