using AUTHIO.INFRASTRUCTURE.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

/// <summary>
/// Extensão de configuração de ServiceBus.
/// </summary>
public static class ServiceBusExtensions
{
    /// <summary>
    /// Configura o subscriber do serviceBus.
    /// </summary>
    /// <param name="webApplication"></param>
    /// <returns></returns>
    public static WebApplication ConfigureServiceBusSubscriber(
        this WebApplication webApplication)
    {
        webApplication.Services
            .GetService<EventServiceBusSubscriber>()
                .RegisterReceiveMessageHandler();

        return webApplication;
    }
}
