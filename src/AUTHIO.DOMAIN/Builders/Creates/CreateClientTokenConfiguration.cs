using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um ClientTokenConfigurationEntity.
/// </summary>
public static class CreateClientTokenConfiguration
{
    /// <summary>
    /// Cria uma instância de ClientTokenConfigurationEntity padrão.
    /// </summary>
    /// <param name="ClientIdentityConfigurationId"></param>
    /// <param name="securityKey"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="encrypted"></param>
    /// <param name="algorithmJwsType"></param>
    /// <param name="algorithmJweType"></param>
    /// <returns></returns>
    public static ClientTokenConfigurationEntity CreateDefault(Guid ClientIdentityConfigurationId, 
        string securityKey, string issuer, string audience, bool encrypted,
        AlgorithmType algorithmJwsType, AlgorithmType algorithmJweType)
            => new ClientTokenConfigurationBuilder()
                .AddClientConfigurationId(ClientIdentityConfigurationId)
                .AddCreated(DateTime.Now)
                .AddSecurityKey(securityKey)
                .AddIssuer(issuer)
                .AddAudience(audience)
                .AddEncrypted(encrypted)
                .AddJwsAlgorithmType(algorithmJwsType)
                .AddJweAlgorithmType(algorithmJweType)
                .Builder();
}
