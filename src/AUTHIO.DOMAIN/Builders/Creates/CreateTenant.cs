using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Classe de criação do tenant.
/// </summary>
public static class CreateTenant
{
    /// <summary>
    /// Cria um tenant com os dados de cadastro inicial.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public static TenantEntity CreateDefault(Guid userId,
        string name, string description,
        TenantConfigurationEntity tenantConfiguration)
        => new TenantBuilder()
            .AddUserId(userId)
            .AddName(name)
            .AddDescription(description)
            .AddStatus(Status.Ativo)
            .AddCreated()
            .AddTenantConfiguration(tenantConfiguration).Builder();
}
