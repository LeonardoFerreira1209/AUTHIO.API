using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração de identity do tenant.
/// </summary>
public static class CreateTenantIdentityConfiguration
{
    /// <summary>
    /// Cria um tenant identity configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static TenantIdentityConfigurationEntity CreateDefault(Guid tenantConfigurationId)
        => new TenantIdentityConfigurationBuilder()
                .AddTenantConfigurationId(tenantConfigurationId)
                    .AddCreated().Builder();
}
