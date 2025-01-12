using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração de identity do Client.
/// </summary>
public static class CreateClientIdentityConfiguration
{
    /// <summary>
    /// Cria um Client identity configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <param name="userIdentityConfiguration"></param>
    /// <param name="passwordIdentityConfiguration"></param>
    /// <param name="lockoutIdentityConfiguration"></param>
    /// <returns></returns>
    public static ClientIdentityConfigurationEntity CreateDefault(Guid ClientConfigurationId,
        UserIdentityConfigurationEntity userIdentityConfiguration,
        PasswordIdentityConfigurationEntity passwordIdentityConfiguration,
        LockoutIdentityConfigurationEntity lockoutIdentityConfiguration)
    => new ClientIdentityConfigurationBuilder()
        .AddClientConfigurationId(ClientConfigurationId)
        .AddCreated()
        .AddUserIdentityConfiguration(userIdentityConfiguration)
        .AddPasswordIdentityConfiguration(passwordIdentityConfiguration)
        .AddLockoutIdentityConfiguration(lockoutIdentityConfiguration)
        .Builder();
}
