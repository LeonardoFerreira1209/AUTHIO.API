using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de TenantTokenConfiguration.
/// </summary>
public sealed class TenantTokenConfigurationBuilder
{
    private Guid tenantConfigurationId;
    private string securityKey;
    private string issuer;
    private string audience;
    private bool encrypted;
    private AlgorithmType algorithmJwsType;
    private AlgorithmType algorithmJweType;
    private DateTime created;
    private DateTime? updated = null;

    /// <summary>
    /// Adiciona um Tenant configuration Id.
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddTenantConfigurationId(Guid tenantConfigurationId)
    {
        this.tenantConfigurationId = tenantConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddCreated(DateTime? created = null)
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
    public TenantTokenConfigurationBuilder AddUpdated(DateTime? updated = null)
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
    public TenantTokenConfigurationBuilder AddSecurityKey(string securityKey)
    {
        this.securityKey = securityKey;

        return this;
    }

    /// <summary>
    /// Adiciona o issuer.
    /// </summary>
    /// <param name="issuer"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddIssuer(string issuer)
    {
        this.issuer = issuer;

        return this;
    }

    /// <summary>
    /// Adiciona o audience.
    /// </summary>
    /// <param name="audience"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddAudience(string audience)
    {
        this.audience = audience;

        return this;
    }

    /// <summary>
    /// Adiciona se o token é encriptado.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddEncrypted(bool encrypted)
    {
        this.encrypted = encrypted;

        return this;
    }

    /// <summary>
    /// Adiciona o type de criptografia do token jws.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddJwsAlgorithmType(AlgorithmType algorithmJwsType)
    {
        this.algorithmJwsType = algorithmJwsType;

        return this;
    }

    /// <summary>
    /// Adiciona o type de criptografia do token jws.
    /// </summary>
    /// <param name="encrypted"></param>
    /// <returns></returns>
    public TenantTokenConfigurationBuilder AddJweAlgorithmType(AlgorithmType algorithmJweType)
    {
        this.algorithmJweType = algorithmJweType;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns>TenantTokenConfigurationEntity</returns>
    public TenantTokenConfigurationEntity Builder()
        => new(tenantConfigurationId, created,
            updated, securityKey,
            issuer, audience, encrypted,
            algorithmJwsType, algorithmJweType
        );
}
