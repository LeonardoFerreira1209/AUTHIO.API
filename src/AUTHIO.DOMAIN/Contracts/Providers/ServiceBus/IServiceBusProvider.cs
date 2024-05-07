using AUTHIO.DOMAIN.Dtos.ServiceBus;

namespace AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;

/// <summary>
/// Interface de provider do service bus.
/// </summary>
public interface IServiceBusProvider
{
    /// <summary>
    /// Envia uma mensagem.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendAsync(MessageBase message);

    /// <summary>
    /// Envia uma mensagem e cabeçalhos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="headers"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task SendAsync<T>(T message, Dictionary<string, object> headers);

    /// <summary>
    /// Envia uma mensagem com data de execução.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task SendScheduleAsync(MessageBase message, DateTime dateTime);
}
