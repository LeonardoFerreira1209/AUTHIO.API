using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Builders.Creates;

/// <summary>
/// Classe de criação de User Identity Configuration.
/// </summary>
public static class CreateUserIdentityConfiguration
{
    /// <summary>
    /// Cria um User tenant configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <returns></returns>
    public static UserIdentityConfigurationEntity CreateDefaultUserIdenityConfiguration(Guid tenantIdentityConfigurationId) 
        => new UserIdentityConfigurationBuilder()
                .AddTenantIdentityConfigurationId(tenantIdentityConfigurationId)
                    .AddRequireUniqueEmail(true)
                        .AddCreated().Builder();
}
