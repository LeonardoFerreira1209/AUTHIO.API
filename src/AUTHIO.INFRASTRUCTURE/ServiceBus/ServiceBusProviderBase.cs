using AUTHIO.DOMAIN.Dtos.ServiceBus;
using AUTHIO.DOMAIN.Helpers.Extensions;
using Azure.Messaging.ServiceBus;
using System.Text;

namespace AUTHIO.INFRASTRUCTURE.ServiceBus;

/// <summary>
/// Classe de provider base do service bus.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public abstract class ServiceBusProviderBase(
    string busConnection, string queueOrTopicName)
{
    /// <summary>
    /// Instancia de ServiceBusClient.
    /// </summary>
    private readonly ServiceBusClient serviceBusClient 
        = new(busConnection);

    /// <summary>
    /// Envia uma mensagem e cabeçalhos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public virtual async Task SendAsync<T>(
        T message, Dictionary<string, object> headers)
    {
        var sender
           = serviceBusClient.CreateSender(queueOrTopicName);

        ServiceBusMessage serviceBusMessage 
            = DataToMessage(message, headers);

        await sender.SendMessageAsync(serviceBusMessage);
    }

    /// <summary>
    /// Envia uma mensagem.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual async Task SendAsync(MessageBase message)
    {
        var sender
          = serviceBusClient.CreateSender(queueOrTopicName);

        ServiceBusMessage serviceBusMessage = DataToMessage(message, message.Headers);

        await sender.SendMessageAsync(serviceBusMessage);
    }

    /// <summary>
    /// Envia uma mensagem com uma data de execução.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public virtual async Task SendScheduleAsync(
        MessageBase message, DateTime dateTime)
    {
        var sender
           = serviceBusClient.CreateSender(queueOrTopicName);

        ServiceBusMessage serviceBusMessage 
            = DataToMessage(message, message.Headers, dateTime);

        await sender.SendMessageAsync(serviceBusMessage);
    }

    /// <summary>
    /// Transforma objeto em uma mensagem.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    public static ServiceBusMessage DataToMessage(
        object data, Dictionary<string, object> headers = null, DateTime? dateTime = null)
    {
        var jsonMessage = data.SerializeIgnoreNullValues();
        var bytesMessage = Encoding.UTF8.GetBytes(jsonMessage);
        var message = new ServiceBusMessage(bytesMessage)
        {
            SessionId = Guid.NewGuid().ToString()
        };

        if(dateTime != null)
            message.ScheduledEnqueueTime = dateTime.Value;

        if (headers != null)
            foreach (var prop in headers)
                message.ApplicationProperties.Add(prop.Key, prop.Value);

        return message;
    }
}
