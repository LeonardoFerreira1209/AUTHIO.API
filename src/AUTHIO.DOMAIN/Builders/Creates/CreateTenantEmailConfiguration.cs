using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um TenantEmailConfigurationEntity.
/// </summary>
public static class CreateTenantEmailConfiguration
{
    /// <summary>
    /// Cria uma instância de TenantEmailConfigurationEntity padrão.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="sendersName"></param>
    /// <param name="sendersEmail"></param>
    /// <param name="isEmailConfirmed"></param>
    /// <returns></returns>
    public static TenantEmailConfigurationEntity CreateDefault(Guid tenantIdentityConfigurationId, 
        string sendersName, string sendersEmail, bool isEmailConfirmed)
            => new TenantEmailConfigurationBuilder()
                .AddTenantConfigurationId(tenantIdentityConfigurationId)
                    .AddCreated(DateTime.Now)
                        .AddSendersName(sendersName)
                            .AddSendersEmail(sendersEmail)
                                .AddIsEmailConfirmed(isEmailConfirmed).Builder();
}
