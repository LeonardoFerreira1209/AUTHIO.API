using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração do tenant.
/// </summary>
public static class CreateTenantConfiguration
{
    /// <summary>
    /// Cria um tenant configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="apikey"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static TenantConfigurationEntity CreateDefaultTenantConfiguration(Guid tenantId) 
        => new TenantConfigurationBuilder()
                .AddApikey($"{$"{Guid.NewGuid()}-HYPER.IO-{Random.Shared.NextInt64(1, 1000)}"}")
                    .AddTenantId(tenantId)
                        .AddCreated().Builder();
}
