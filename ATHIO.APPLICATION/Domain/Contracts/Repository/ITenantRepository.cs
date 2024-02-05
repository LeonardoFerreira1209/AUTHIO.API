using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Entity;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repository;

/// <summary>
/// Repositorio de Tenants.
/// </summary>
public interface ITenantRepository 
    : IGenerictEntityCoreRepository<TenantEntity> 
{

}
