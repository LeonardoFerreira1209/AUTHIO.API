using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um SendGridConfigurationEntity.
/// </summary>
public static class CreateSendGridConfiguration
{
    /// <summary>
    ///  Cria uma instância de SendGridConfigurationEntity padrão.
    /// </summary>
    /// <param name="ClientEmailConfigurationId"></param>
    /// <param name="sendGridApiKey"></param>
    /// <param name="welcomeTemplateId"></param>
    /// <returns></returns>
    public static SendGridConfigurationEntity CreateDefault(Guid ClientEmailConfigurationId, 
        string sendGridApiKey, string welcomeTemplateId)
            => new SendGridConfigurationBuilder()
                .AddClientEmailConfigurationId(ClientEmailConfigurationId)
                .AddCreated(DateTime.Now)
                .AddSendGridApiKey(sendGridApiKey)
                .AddWelcomeTemplateId(welcomeTemplateId)
                .Builder();
}
