using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um evento.
/// </summary>
public sealed class CreateEvent
{
    /// <summary>
    ///  Cria uma instância de evento de email.
    /// </summary>
    /// <param name="jsonBody"></param>
    /// <returns></returns>
    public static EventEntity CreateEmailEvent(string jsonBody)
        => new EventBuilder()
            .AddCreated(DateTime.Now)
            .AddJsonBody(jsonBody)
            .AddType(EventType.Email)
            .Builder();
}
