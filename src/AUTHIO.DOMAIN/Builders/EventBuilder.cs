using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de eventos.
/// </summary>
public sealed class EventBuilder
{
    private DateTime created;
    private DateTime? processed;
    private DateTime? sended;
    private DateTime schedulerTime;
    private string jsonBody;
    private EventType type;

    /// <summary>
    /// Adiciona data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public EventBuilder AddCreated(DateTime created)
    {
        this.created = created;

        return this;
    }

    /// <summary>
    /// Adiciona data de processamento.
    /// </summary>
    /// <param name="processed"></param>
    /// <returns></returns>
    public EventBuilder AddProcessed(DateTime? processed)
    {
        this.processed
            = processed
            ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona data de envio.
    /// </summary>
    /// <param name="sended"></param>
    /// <returns></returns>
    public EventBuilder AddSended(DateTime? sended)
    {
        this.sended
            = sended
            ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona data de execução.
    /// </summary>
    /// <param name="schedulerTime"></param>
    /// <returns></returns>
    public EventBuilder AddSchedulerTime(DateTime schedulerTime)
    {
        this.schedulerTime = schedulerTime;

        return this;
    }

    /// <summary>
    /// Adiciona o corpo da mensagem.
    /// </summary>
    /// <param name="jsonBody"></param>
    /// <returns></returns>
    public EventBuilder AddJsonBody(string jsonBody)
    {
        this.jsonBody = jsonBody;

        return this;
    }

    /// <summary>
    /// Adiciona o tipo do evento.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public EventBuilder AddType(EventType type)
    {
        this.type = type;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public EventEntity Builder()
        => new(created, processed, sended,
            schedulerTime, jsonBody, type);
}
