namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de password identity confiurations.
/// </summary>
public sealed class UpdatePasswordIdentityConfigurationRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Tamanho requerido.
    /// </summary>
    public int RequiredLength { get; set; }

    /// <summary>
    /// caracteres unuicos requeridos.
    /// </summary>
    public int RequiredUniqueChars { get; set; }

    /// <summary>
    /// Alpha numericos requeridos.
    /// </summary>
    public bool RequireNonAlphanumeric { get; set; }

    /// <summary>
    /// Minusculo requerido.
    /// </summary>
    public bool RequireLowercase { get; set; }

    /// <summary>
    /// Maiusculo requerido.
    /// </summary>
    public bool RequireUppercase { get; set; }

    /// <summary>
    /// Digito requerido.
    /// </summary>
    public bool RequireDigit { get; set; }
}
