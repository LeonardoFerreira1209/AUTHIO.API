using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de Configuração de token do Client.
/// </summary>
public class ClientTokenConfigurationEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public ClientTokenConfigurationEntity()
    {

    }

    /// <summary>
    ///  ctor
    /// </summary>
    /// <param name="clientConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="securityKey"></param>
    /// <param name="issuer"></param>
    /// <param name="audience"></param>
    /// <param name="encrypted"></param>
    /// <param name="algorithmJwsType"></param>
    /// <param name="algorithmJweType"></param>
    public ClientTokenConfigurationEntity(
        Guid clientConfigurationId, DateTime created,
        DateTime? updated, string securityKey, string issuer,
        string audience, bool encrypted,
        AlgorithmType algorithmJwsType, AlgorithmType algorithmJweType)
    {
        ClientConfigurationId = clientConfigurationId;
        Created = created;
        Updated = updated;
        SecurityKey = securityKey;
        Issuer = issuer;
        Audience = audience;
        Encrypted = encrypted;
        AlgorithmJwsType = algorithmJwsType;
        AlgorithmJweType = algorithmJweType;
    }

    /// <summary>
    /// User Id
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
    /// Id do Client configuration Id.
    /// </summary>
    public Guid ClientConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client configuration.
    /// </summary>
    public virtual ClientConfigurationEntity ClientConfiguration { get; private set; }

}
