namespace AUTHIO.DOMAIN.Contracts.Providers.ServiceBus;

/// <summary>
/// 
/// </summary>
public interface IEventServiceBusSubscriber
{
    /// <summary>
    /// 
    /// </summary>
    public void RegisterReceiveMessageHandler();
}
