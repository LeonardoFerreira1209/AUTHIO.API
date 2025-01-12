using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração do Client.
/// </summary>
public static class CreateClientConfiguration
{
    /// <summary>
    /// Cria um Client configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="ClientId"></param>
    /// <returns></returns>
    public static ClientConfigurationEntity CreateDefault(Guid ClientId, 
        ClientIdentityConfigurationEntity ClientIdentityConfiguration, 
        ClientEmailConfigurationEntity ClientEmailConfiguration, 
        ClientTokenConfigurationEntity ClientTokenConfiguration
    )
        => new ClientConfigurationBuilder()
            .AddClientKey($"{$"{Guid.NewGuid()}-HYPER.IO-{Random.Shared.NextInt64(1, 1000)}"}")
            .AddClientId(ClientId)
            .AddCreated()
            .AddClientIdentityConfiguration(ClientIdentityConfiguration)
            .AddClientEmailConfiguration(ClientEmailConfiguration)
            .AddClientTokenConfiguration(ClientTokenConfiguration)
            .Builder();
}
