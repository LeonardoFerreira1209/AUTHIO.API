using AUTHIO.DATABASE.Context;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.Email;
using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Email;
using AUTHIO.DOMAIN.Dtos.ServiceBus.Events;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.INFRASTRUCTURE.ServiceBus.Base;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace AUTHIO.INFRASTRUCTURE.ServiceBus;

/// <summary>
/// Serviço de subscriber de eventos.
/// </summary>
/// <param name="appSettings"></param>
/// <param name="emailProviderFactory"></param>
public class EventServiceBusSubscriber(
    IServiceProvider serviceProvider,
    IOptions<AppSettings> appSettings,
    IEmailProviderFactory emailProviderFactory) 
    : ServiceBusSubscriberBase(appSettings, QUEUE_OR_TOPIC_NAME), IServiceBusSubscriber
{
    /// <summary>
    /// Const de nome da queue ou topico.
    /// </summary>
    private const string QUEUE_OR_TOPIC_NAME = "events";

    /// <summary>
    /// String de conexão do service bus.
    /// </summary>
    private readonly string busConnection = Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION_STRING")
            ?? appSettings.Value.ServiceBus.ConnectionString;

    /// <summary>
    /// Provider de email.
    /// </summary>
    private readonly IEmailProvider emailProvider = emailProviderFactory.GetSendGridEmailProvider();

    /// <summary>
    /// Processa as mensagens.
    /// </summary>
    /// <param name="messageEvent">Dados da mensagem do evento.</param>
    /// <returns>Task</returns>
    public override async Task ProcessMensageAsync(
        ProcessMessageEventArgs messageEvent)
    {
        var message = messageEvent.Message;

        try
        {
            EventMessage<object> eventMessage = 
                JsonConvert.DeserializeObject<EventMessage<object>>(
                    message.Body.ToString());

            await ProccesByEventTypeAsync(
                eventMessage.EventType, eventMessage.Data)
                    .ContinueWith(async (task) => {

                        using var scope 
                            = serviceProvider.CreateScope();

                        var eventRepository 
                            = scope.ServiceProvider
                                .GetService<IEventRepository>();

                        await eventRepository.GetAsync(even
                            => even.Id == eventMessage.Id).ContinueWith(async (evenTask) =>
                            {
                                var eventEntity
                                    = evenTask.Result;

                                eventEntity.Processed 
                                    = DateTime.Now;

                                await eventRepository
                                    .UpdateAsync(eventEntity);

                                var unitOfWork = scope.ServiceProvider
                                    .GetService<IUnitOfWork<AuthIoContext>>();

                                await unitOfWork.CommitAsync();

                            }).Unwrap();

                    }).Unwrap();

            Log.Information(
                $"[LOG INFORMATION] - {nameof(EventServiceBusSubscriber)} - METHOD {nameof(ProcessMensageAsync)} - Memsagem consumida com sucesso: {JsonConvert.SerializeObject(eventMessage.Data)}.\n");
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            var cloneMessage = new ServiceBusMessage(messageEvent.Message) {
                ScheduledEnqueueTime 
                    = DateTime.UtcNow.AddHours(1)
            };

            ServiceBusSender sender = 
                _busClient.CreateSender(QUEUE_OR_TOPIC_NAME);

            await sender.SendMessageAsync(cloneMessage);
            await sender.CloseAsync();

            throw;
        }
    }

    /// <summary>
    /// Processa a mensagem com base no tipo de evento.
    /// </summary>
    /// <param name="eventType">Tipo de evento</param>
    /// <param name="data">Dados do evento</param>
    /// <returns>Task</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task ProccesByEventTypeAsync(EventType eventType, object data)
    {
        switch(eventType)
        {
            case EventType.Email:
                await ProccessEmailAsync(data); 
                break;
            default:
                throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Processa mensagens de e-mail.
    /// </summary>
    /// <param name="data">Mensagem de e-mail a ser processada.</param>
    /// <returns>Task.</returns>
    private async Task ProccessEmailAsync(
        object data)
    {
        if (data == null) 
            throw new Exception("Mensagem de e-mail recebida é nula");

        var defaultEmailMessage
            = JsonConvert.DeserializeObject<DefaultEmailMessage>(
                JsonConvert.SerializeObject(data));

        await emailProvider
            .SendEmailAsync(defaultEmailMessage);

        Log.Information(
                $"[LOG INFORMATION] - {nameof(EventServiceBusSubscriber)} - METHOD {nameof(ProccessEmailAsync)} - Email enviado com sucesso: {JsonConvert.SerializeObject(defaultEmailMessage)}.\n");

        await Task.CompletedTask;
    }
}
