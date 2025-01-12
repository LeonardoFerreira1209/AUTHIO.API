using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um ClientEmailConfigurationEntity.
/// </summary>
public static class CreateClientEmailConfiguration
{
    /// <summary>
    /// Cria uma instância de ClientEmailConfigurationEntity padrão.
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <param name="sendersName"></param>
    /// <param name="sendersEmail"></param>
    /// <param name="isEmailConfirmed"></param>
    /// <param name="sendGridConfiguration"></param>
    /// <returns></returns>
    public static ClientEmailConfigurationEntity CreateDefault(Guid ClientIdentityConfigurationId,
        string sendersName,
        string sendersEmail,
        bool isEmailConfirmed,
        SendGridConfigurationEntity sendGridConfiguration)
    => new ClientEmailConfigurationBuilder()
        .AddClientConfigurationId(ClientIdentityConfigurationId)
        .AddCreated(DateTime.Now)
        .AddSendersName(sendersName)
        .AddSendersEmail(sendersEmail)
        .AddIsEmailConfirmed(isEmailConfirmed)
        .AddSendGridConfiguration(sendGridConfiguration)
        .Builder();
}
