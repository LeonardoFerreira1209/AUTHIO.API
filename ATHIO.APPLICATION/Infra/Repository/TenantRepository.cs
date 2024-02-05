using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Infra.Context;
using AUTHIO.APPLICATION.Infra.Repository.Base;

namespace AUTHIO.APPLICATION.Infra.Repository;

/// <summary>
/// Repositorio de Tenants.
/// </summary>
public sealed class TenantRepository(
    AuthIoContext context) 
        : GenericEntityCoreRepository<TenantEntity>(context), ITenantRepository
{
   
}
