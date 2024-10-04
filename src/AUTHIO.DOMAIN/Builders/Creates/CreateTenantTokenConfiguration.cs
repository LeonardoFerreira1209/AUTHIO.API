using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um TenantTokenConfigurationEntity.
/// </summary>
public static class CreateTenantTokenConfiguration
{
    /// <summary>
    /// Cria uma instância de TenantTokenConfigurationEntity padrão.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <param name="securityKey"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="encrypted"></param>
    /// <param name="algorithmJwsType"></param>
    /// <param name="algorithmJweType"></param>
    /// <returns></returns>
    public static TenantTokenConfigurationEntity CreateDefault(Guid tenantIdentityConfigurationId, 
        string securityKey, string issuer, string audience, bool encrypted,
        AlgorithmType algorithmJwsType, AlgorithmType algorithmJweType)
            => new TenantTokenConfigurationBuilder()
                .AddTenantConfigurationId(tenantIdentityConfigurationId)
                .AddCreated(DateTime.Now)
                .AddSecurityKey(securityKey)
                .AddIssuer(issuer)
                .AddAudience(audience)
                .AddEncrypted(encrypted)
                .AddJwsAlgorithmType(algorithmJwsType)
                .AddJweAlgorithmType(algorithmJweType)
                .Builder();
}
