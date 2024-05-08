using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de eventos.
/// </summary>
public sealed class EventRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<EventEntity>(context), IEventRepository
{ }
