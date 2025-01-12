using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de User Identity Configuration.
/// </summary>
public static class CreateUserIdentityConfiguration
{
    /// <summary>
    /// Cria um User Client configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <returns></returns>
    public static UserIdentityConfigurationEntity CreateDefault(Guid ClientIdentityConfigurationId)
        => new UserIdentityConfigurationBuilder()
                .AddClientIdentityConfigurationId(ClientIdentityConfigurationId)
                    .AddRequireUniqueEmail(true)
                        .AddCreated().Builder();
}
