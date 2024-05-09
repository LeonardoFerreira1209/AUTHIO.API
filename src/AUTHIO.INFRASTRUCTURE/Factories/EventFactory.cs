using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Dtos.ServiceBus.Events;
using AUTHIO.DOMAIN.Enums;
using Newtonsoft.Json;

namespace AUTHIO.INFRASTRUCTURE.Factories;

/// <summary>
/// Factory de mensagens de eventos.
/// </summary>
public class EventFactory : IEventFactory
{
    /// <summary>
    /// Cria uma mensagem de evento.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventType"></param>
    /// <param name="jsonBody"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public EventMessage<T> CreateEventMessage<T>(
        EventType eventType, string jsonBody, Guid eventId) where T : class
    {
        var deserializeObject
            = JsonConvert.DeserializeObject<T>(jsonBody);

        return eventType switch
        {
            EventType.Email
                => new EventMessage<T>(
                    deserializeObject, eventType, eventId),
            _ => 
                throw new ArgumentException(
                    "Tipo de evento desconhecido")
        };
    }
}
