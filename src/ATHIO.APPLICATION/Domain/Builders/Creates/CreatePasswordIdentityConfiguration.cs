using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração da Senha.
/// </summary>
public static class CreatePasswordIdentityConfiguration
{
    /// <summary>
    /// Cria um password identity configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <returns></returns>
    public static PasswordIdentityConfigurationEntity CreateDefault(Guid tenantConfigurationId)
        => new PasswordIdentityConfigurationBuilder()
                .AddRequiredLength(15)
                    .AddRequiredUniqueChars(1)
                        .AddRequireNonAlphanumeric(true)
                            .AddRequireLowercase(true)
                                .AddRequireUppercase(true)
                                    .AddRequireDigit(true)
                                        .AddTenantIdentityConfigurationId(tenantConfigurationId)
                                            .AddCreated()
                                                .Builder();
}
