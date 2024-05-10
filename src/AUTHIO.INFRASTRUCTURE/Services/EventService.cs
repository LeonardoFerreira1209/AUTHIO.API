﻿using AUTHIO.DATABASE.Context;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
﻿using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Email;
using AUTHIO.DOMAIN.Entities;
using Newtonsoft.Json;
using Serilog;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de eventos.
/// </summary>
/// <param name="eventFactory"></param>
/// <param name="eventServiceBusProvider"></param>
/// <param name="eventRepository"></param>
public class EventService(
    IEventFactory eventFactory,
    IEventServiceBusProvider eventServiceBusProvider,
    IUnitOfWork<AuthIoContext> unitOfWork,
    IEventRepository eventRepository) : IEventService
{
    /// <summary>
    /// Envia um evento para o service bus.
    /// </summary>
    /// <returns></returns>
    public async Task SendEventsToBusAsync()
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(AuthenticationService)} - METHOD {nameof(SendEventsToBusAsync)}\n");

        try
        {
            DateTime currentDate
                = DateTime.Now;

            ICollection<EventEntity> events
                = await eventRepository.GetAllAsync(
                    x => x.SchedulerTime <= currentDate && x.Sended == null);

            foreach (EventEntity even in events )
            {
                var message =
                       eventFactory.CreateEventMessage<EmailMessage>(
                           even.Type, even.JsonBody, even.Id);

                await eventServiceBusProvider
                    .SendAsync(message).ContinueWith(async (Task) =>
                    {
                        even.Sended = DateTime.Now;
                        await eventRepository.UpdateAsync(even);
                    });
            }

            await unitOfWork
                    .CommitAsync();
                    x => x.SchedulerTime <= currentDate && x.Processed == null);

            events.AsParallel()
                .ForAll(async (even) =>
                {
                    var message =
                        eventFactory.CreateEventMessage<EmailMessage>(
                            even.Type, even.JsonBody, even.Id);

                    await eventServiceBusProvider.SendAsync(message);
                });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }
}
