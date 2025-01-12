using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositório de Client Identity Configuration Reopository.
/// </summary>
/// <param name="context"></param>
public sealed class ClientIdentityConfigurationRepository(AuthIoContext context)
    : GenericEntityCoreRepository<ClientIdentityConfigurationEntity>(context), IClientIdentityConfigurationRepository
{

}
