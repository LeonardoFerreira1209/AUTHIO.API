using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

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
    public static TenantConfigurationEntity CreateDefaultTenantConfiguration(string apikey, Guid tenantId) 
        => new TenantConfigurationBuilder()
                .AddApikey(apikey)
                    .AddTenantId(tenantId)
                        .AddCreated()
                            .AddStatus(Status.Ativo).Builder();
}
