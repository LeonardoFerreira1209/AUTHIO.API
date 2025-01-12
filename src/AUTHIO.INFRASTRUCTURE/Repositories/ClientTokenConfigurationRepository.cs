using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Client token configurations.
/// </summary>
public sealed class ClientTokenConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<ClientTokenConfigurationEntity>(context), IClientTokenConfigurationRepository
{ }
