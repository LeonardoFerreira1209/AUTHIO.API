using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração do tenant.
/// </summary>
public static class CreateTenantConfiguration
{
    /// <summary>
    /// Cria um tenant configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static TenantConfigurationEntity CreateDefault(Guid tenantId, 
        TenantIdentityConfigurationEntity tenantIdentityConfiguration, 
        TenantEmailConfigurationEntity tenantEmailConfiguration, 
        TenantTokenConfigurationEntity tenantTokenConfiguration
    )
        => new TenantConfigurationBuilder()
            .AddTenantKey($"{$"{Guid.NewGuid()}-HYPER.IO-{Random.Shared.NextInt64(1, 1000)}"}")
            .AddTenantId(tenantId)
            .AddCreated()
            .AddTenantIdentityConfiguration(tenantIdentityConfiguration)
            .AddTenantEmailConfiguration(tenantEmailConfiguration)
            .AddTenantTokenConfiguration(tenantTokenConfiguration)
            .Builder();
}
