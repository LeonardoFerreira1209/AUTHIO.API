namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de token configuration.
/// </summary>
public sealed class UpdateTenantTokenConfigurationRequest
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public required Guid Id { get; set; }

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
}
