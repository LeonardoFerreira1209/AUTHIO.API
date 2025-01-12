using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação do Client.
/// </summary>
public static class CreateClient
{
    /// <summary>
    /// Cria um Client com os dados de cadastro inicial.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static ClientEntity CreateDefault(Guid userId,
        string name, string description,
        ClientConfigurationEntity ClientConfiguration)
        => new ClientBuilder()
            .AddUserId(userId)
            .AddName(name)
            .AddDescription(description)
            .AddStatus(Status.Ativo)
            .AddCreated()
            .AddClientConfiguration(ClientConfiguration).Builder();
}
