using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de eventos.
/// </summary>
public class EventEntity
{
    /// <summary>
    /// Id do evento.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de procesamento.
    /// </summary>
    public DateTime Processed { get; set; }

    /// <summary>
    /// Dados do evento.
    /// </summary>
    public dynamic Body { get; set; }

    /// <summary>
    /// Tipo do evento.
    /// </summary>
    public EventType Type { get; set; }
}
