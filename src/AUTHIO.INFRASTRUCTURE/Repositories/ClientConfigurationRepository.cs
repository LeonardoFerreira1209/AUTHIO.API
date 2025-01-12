using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Client configurations.
/// </summary>
public sealed class ClientConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<ClientConfigurationEntity>(context), IClientConfigurationRepository
{ }
