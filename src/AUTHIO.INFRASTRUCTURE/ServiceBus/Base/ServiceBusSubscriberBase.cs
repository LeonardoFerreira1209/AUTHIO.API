using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace AUTHIO.INFRASTRUCTURE.ServiceBus.Base;

/// <summary>
/// Processamento de mensagens do bus.
/// </summary>
/// <param name="appSettings"></param>
/// <param name="queueOrTopicName"></param>
public abstract class ServiceBusSubscriberBase(
    IOptions<AppSettings> appSettings, string queueOrTopicName) : IServiceBusSubscriber
{
    /// <summary>
    /// Instancia de ServiceBusCient.
    /// </summary>
    public ServiceBusClient _busClient;

    /// <summary>
    /// String de conexão do service bus.
    /// </summary>
    private readonly string busConnection
        = Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION_STRING")
            ?? appSettings.Value.ServiceBus.ConnectionString;

    /// <summary>
    /// Registra o handler de recebimento de mensagens.
    /// </summary>
    public void RegisterReceiveMessageHandler()
    {
        Log.Information(
           $"[LOG INFORMATION] - SET TITLE {nameof(EventServiceBusSubscriber)} - METHOD {nameof(RegisterReceiveMessageHandler)} - Subscriber inicializado\n");

        _busClient =
            new ServiceBusClient(busConnection);

        var messageHandlerOptions
            = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = true,
            };

        try
        {
            ServiceBusProcessor processor
                = _busClient.CreateProcessor(
                    queueOrTopicName, messageHandlerOptions);

            processor.ProcessMessageAsync += ProcessMensageAsync;
            processor.ProcessErrorAsync += ProcessErrorAsync;

            processor.StartProcessingAsync();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Método abstrato de processamento de mesnagens do bus.
    /// </summary>
    /// <param name="messageEvent"></param>
    /// <returns></returns>
    public abstract Task ProcessMensageAsync(ProcessMessageEventArgs messageEvent);

    /// <summary>
    /// Processa erros durante a recepção de mensagens.
    /// </summary>
    /// <param name="exceptionReceivedEventArgs">Dados do evento de exceção.</param>
    /// <returns>Task.</returns>
    private Task ProcessErrorAsync(
        ProcessErrorEventArgs exceptionReceivedEventArgs)
    {
        var context
            = exceptionReceivedEventArgs.Exception;

        Log.Error($"[LOG ERROR] - Exception: {context.Message} - {JsonConvert.SerializeObject(context)}\n");

        return Task.CompletedTask;
    }
}
