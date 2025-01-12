using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de Client email configurations.
/// </summary>
public sealed class ClientEmailConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<ClientEmailConfigurationEntity>(context), IClientEmailConfigurationRepository
{ }
