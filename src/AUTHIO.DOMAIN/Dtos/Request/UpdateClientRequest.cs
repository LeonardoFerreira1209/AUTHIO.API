namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Client.
/// </summary>
public sealed class UpdateClientRequest
{
    /// <summary>
    /// Id do Client.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nomde do Client.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Descrição do Client.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Classe de atualização de configuração de Client.
    /// </summary>
    public UpdateClientConfigurationRequest ClientConfiguration { get; set; }
}
