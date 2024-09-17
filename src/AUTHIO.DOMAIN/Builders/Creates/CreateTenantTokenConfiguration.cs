using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um TenantTokenConfigurationEntity.
/// </summary>
public static class CreateTenantTokenConfiguration
{
    /// <summary>
    ///  Cria uma instância de TenantTokenConfigurationEntity padrão.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="securityKey"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <returns>TenantTokenConfigurationEntity</returns>
    public static TenantTokenConfigurationEntity CreateDefault(Guid tenantIdentityConfigurationId, 
        string securityKey, string issuer, string audience)
            => new TenantTokenConfigurationBuilder()
                .AddTenantConfigurationId(tenantIdentityConfigurationId)
                .AddCreated(DateTime.Now)
                .AddSecurityKey(securityKey)
                .AddIssuer(issuer)
                .AddAudience(audience)
                .Builder();
}
