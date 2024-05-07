using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.ServiceBus.Events;

/// <summary>
/// Classe de envio de mensagem de evento.
/// </summary>
public class EventMessage<T>(T data) 
    : MessageBase(EventType.Email) where T : class 
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
