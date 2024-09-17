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
    /// <param name="tenantEmailConfigurationId"></param>
    /// <param name="sendGridApiKey"></param>
    /// <param name="welcomeTemplateId"></param>
    /// <returns></returns>
    public static SendGridConfigurationEntity CreateDefault(Guid tenantEmailConfigurationId, 
        string sendGridApiKey, string welcomeTemplateId)
            => new SendGridConfigurationBuilder()
                .AddTenantEmailConfigurationId(tenantEmailConfigurationId)
                .AddCreated(DateTime.Now)
                .AddSendGridApiKey(sendGridApiKey)
                .AddWelcomeTemplateId(welcomeTemplateId)
                .Builder();
}
