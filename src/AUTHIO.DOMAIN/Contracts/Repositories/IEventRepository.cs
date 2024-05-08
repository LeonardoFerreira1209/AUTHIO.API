using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de eventos.
/// </summary>
public interface IEventRepository
    : IGenerictEntityCoreRepository<EventEntity>
{ }
