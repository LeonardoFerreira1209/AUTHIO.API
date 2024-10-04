using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de TenantTokenConfiguration.
/// </summary>
public class TenantTokenConfigurationResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// String de segurança para validação do token.
    /// </summary>
    public string SecurityKey { get; set; }

    /// <summary>
    /// String de issuer valido para validação do token.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// String de audience valido para validação do token.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Token deve ser encriptado.
    /// </summary>
    public bool Encrypted { get; set; }

    /// <summary>
    /// Tipo do algoritimo do token jws.
    /// </summary>
    public AlgorithmType AlgorithmJwsType { get; set; }

    /// <summary>
    /// Tipo do algoritimo do token jwe.
    /// </summary>
    public AlgorithmType AlgorithmJweType { get; set; }


    /// <summary>
    /// Id do tenant configuration Id.
    /// </summary>
    public Guid TenantConfigurationId { get; set; }

    /// <summary>
    /// Response do tenant configuration.
    /// </summary>
    public TenantConfigurationResponse TenantConfiguration { get; set; }
}
