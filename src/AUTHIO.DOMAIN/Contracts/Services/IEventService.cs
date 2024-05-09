namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de serviço de Evento.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Envia um evento para o service bus.
    /// </summary>
    /// <returns></returns>
    Task SendEventsToBusAsync();
}
