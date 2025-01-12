using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de ClientTokenConfiguration.
/// </summary>
public sealed class ClientTokenConfigurationBuilder
{
    private Guid ClientConfigurationId;
    private string securityKey;
    private string issuer;
    private string audience;
    private bool encrypted;
    private AlgorithmType algorithmJwsType;
    private AlgorithmType algorithmJweType;
    private DateTime created;
    private DateTime? updated = null;

    /// <summary>
    /// Adiciona um Client configuration Id.
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddClientConfigurationId(Guid ClientConfigurationId)
    {
        this.ClientConfigurationId = ClientConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddCreated(DateTime? created = null)
    {
        this.created
            = created
            ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona a data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona o securityKey.
    /// </summary>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddSecurityKey(string securityKey)
    {
        this.securityKey = securityKey;

        return this;
    }

    /// <summary>
    /// Adiciona o issuer.
    /// </summary>
    /// <param name="issuer"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddIssuer(string issuer)
    {
        this.issuer = issuer;

        return this;
    }

    /// <summary>
    /// Adiciona o audience.
    /// </summary>
    /// <param name="audience"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddAudience(string audience)
    {
        this.audience = audience;

        return this;
    }

    /// <summary>
    /// Adiciona se o token é encriptado.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddEncrypted(bool encrypted)
    {
        this.encrypted = encrypted;

        return this;
    }

    /// <summary>
    /// Adiciona o type de criptografia do token jws.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddJwsAlgorithmType(AlgorithmType algorithmJwsType)
    {
        this.algorithmJwsType = algorithmJwsType;

        return this;
    }

    /// <summary>
    /// Adiciona o type de criptografia do token jws.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public ClientTokenConfigurationBuilder AddJweAlgorithmType(AlgorithmType algorithmJweType)
    {
        this.algorithmJweType = algorithmJweType;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns>ClientTokenConfigurationEntity</returns>
    public ClientTokenConfigurationEntity Builder()
        => new(ClientConfigurationId, created,
            updated, securityKey,
            issuer, audience, encrypted,
            algorithmJwsType, algorithmJweType
        );
}
