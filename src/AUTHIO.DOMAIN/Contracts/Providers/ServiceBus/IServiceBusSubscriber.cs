namespace AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;

/// <summary>
/// Interface de subscriber do bus.
/// </summary>
public interface IServiceBusSubscriber
{
    /// <summary>
    /// Metodo de registro e start do subscriber.
    /// </summary>
    public void RegisterReceiveMessageHandler();
}
