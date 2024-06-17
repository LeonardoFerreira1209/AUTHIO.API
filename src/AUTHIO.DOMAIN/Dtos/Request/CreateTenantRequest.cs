namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de criação de Tenant.
/// </summary>
public sealed class CreateTenantRequest
{
    /// <summary>
    /// Nomde do tenant.
    /// </summary>
    public required string Name { get; set; } = null;

    /// <summary>
    /// Descrição do tenant.
    /// </summary>
    public string Description { get; set; } = null;

    /// <summary>
    /// Email do tenant.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Chave de Api do SendGrid.
    /// </summary>
    public string SendGridApiKey { get; set; } = null;

    /// <summary>
    /// Id do template de bem vindo e confirmação de email do usuário.
    /// </summary>
    public string WelcomeTemplateId { get; set; } = null;

    /// <summary>
    /// Configurações do token.
    /// </summary>
    public TokenConfigurationRequest TokenConfigurationRequest { get; set; }
}

/// <summary>
/// Record de request de configurações do token.
/// </summary>
/// <param name="SecurityKey"></param>
/// <param name="Issuer"></param>
/// <param name="Audience"></param>
public record TokenConfigurationRequest(string SecurityKey, string Issuer, string Audience);