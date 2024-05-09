using AUTHIO.DOMAIN.Enums;
using System.Text.Json.Serialization;

namespace AUTHIO.DOMAIN.Dtos.ServiceBus;

/// <summary>
/// Classe de mensagem base.
/// </summary>
/// <param name="eventType"></param>
/// <param name="eventId"></param>
public class MessageBase(
    EventType eventType, Guid? eventId = null)
{
    /// <summary>
    /// Id do evento.
    /// </summary>
    public Guid? Id { get; set; } = eventId ?? Guid.NewGuid();

    /// <summary>
    /// Tipo do evento.
    /// </summary>
    public EventType EventType { get; set; } = eventType;

    /// <summary>
    /// Cabeçalhos.
    /// </summary>
    [JsonIgnore] 
    public Dictionary<string, object> Headers { get; set; }
}
