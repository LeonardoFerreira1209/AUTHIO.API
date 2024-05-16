using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.INFRASTRUCTURE.ServiceBus.Base;
using Microsoft.Extensions.Options;

namespace AUTHIO.INFRASTRUCTURE.ServiceBus;

/// <summary>
/// Classe de provider barramento de mensagem de eventos.
/// </summary>
public class EventServiceBusProvider(IOptions<AppSettings> appsettings)
    : ServiceBusProviderBase(
        Environment.GetEnvironmentVariable("SERVICEBUS_CONNECTION_STRING") 
            ?? appsettings.Value.ServiceBus.ConnectionString, "events"), IEventServiceBusProvider
{

}
