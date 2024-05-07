using AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;
using AUTHIO.DOMAIN.Dtos.Configurations;
using Microsoft.Extensions.Options;

namespace AUTHIO.INFRASTRUCTURE.ServiceBus;

/// <summary>
/// Classe de provider barramento de mensagem de eventos.
/// </summary>
public class EventServiceBusProvider(IOptions<AppSettings> appsettings)
    : ServiceBusProviderBase(
        appsettings.Value.ServiceBus.ConnectionString 
            ?? Environment.GetEnvironmentVariable(
                "SERVICEBUS_CONNECTION_STRING"), "events"), IEventServiceBusProvider
{

}
