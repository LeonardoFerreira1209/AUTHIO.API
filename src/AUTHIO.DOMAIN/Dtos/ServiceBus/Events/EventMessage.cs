using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.ServiceBus.Events;

/// <summary>
/// Classe de envio de mensagem de evento.
/// </summary>
public class EventMessage<T>(T data, EventType eventType, Guid? eventId)
    : MessageBase(eventType, eventId) where T : class
{
    /// <summary>
    /// Dados do evento.
    /// </summary>
    public T Data { get; set; } = data;

    /// <summary>
    /// Data de criação do evento.
    /// </summary>
    public DateTime Created { get; set; } = DateTime.Now;
}
