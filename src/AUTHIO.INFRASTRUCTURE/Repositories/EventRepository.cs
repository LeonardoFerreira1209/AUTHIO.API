using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.BASE;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de eventos.
/// </summary>
public sealed class EventRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<EventEntity>(context), IEventRepository
{ }
