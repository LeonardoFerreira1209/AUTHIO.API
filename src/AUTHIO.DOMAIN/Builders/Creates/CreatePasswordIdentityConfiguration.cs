using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração da Senha.
/// </summary>
public static class CreatePasswordIdentityConfiguration
{
    /// <summary>
    /// Cria um password identity configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <returns></returns>
    public static PasswordIdentityConfigurationEntity CreateDefault(Guid ClientConfigurationId)
        => new PasswordIdentityConfigurationBuilder()
            .AddRequiredLength(15)
            .AddRequiredUniqueChars(1)
            .AddRequireNonAlphanumeric(true)
            .AddRequireLowercase(true)
            .AddRequireUppercase(true)
            .AddRequireDigit(true)
            .AddClientIdentityConfigurationId(ClientConfigurationId)
            .AddCreated()
            .Builder();
}
