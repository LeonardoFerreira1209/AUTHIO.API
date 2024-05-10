using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Entidade de eventos.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class EventEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public EventEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="created"></param>
    /// <param name="processed"></param>
    /// <param name="sended"></param>
    /// <param name="schedulerTime"></param>
    /// <param name="jsonBody"></param>
    /// <param name="type"></param>
    public EventEntity(
        DateTime created, DateTime? processed, DateTime? sended,
        DateTime schedulerTime, string jsonBody, EventType type)
    {
        Created = created;
        Processed = processed;
        SchedulerTime = schedulerTime;
        JsonBody = jsonBody;
        Type = type;
    }

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
    public DateTime? Processed { get; set; }

    /// <summary>
    /// Data de envio.
    /// </summary>
    public DateTime? Sended { get; set; }

    /// <summary>
    /// Tempo de execução.
    /// </summary>
    public DateTime SchedulerTime { get; set; }

    /// <summary>
    /// Dados do evento.
    /// </summary>
    public string JsonBody { get; set; }

    /// <summary>
    /// Tipo do evento.
    /// </summary>
    public EventType Type { get; set; }
}
