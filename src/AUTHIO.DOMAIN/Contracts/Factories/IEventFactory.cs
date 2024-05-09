using AUTHIO.DOMAIN.Dtos.ServiceBus.Events;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Contracts.Factories;

/// <summary>
/// Interface de Factory de eventos.
/// </summary>
public interface IEventFactory
{
    /// <summary>
    /// Cria uma instância de EventMessage.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="jsonBody"></param>
    /// <returns></returns>
    EventMessage<T> CreateEventMessage<T>(
        EventType eventType, string jsonBody, Guid eventId) where T : class; 
}
