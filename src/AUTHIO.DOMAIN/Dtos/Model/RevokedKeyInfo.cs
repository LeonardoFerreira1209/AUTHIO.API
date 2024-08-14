namespace AUTHIO.DOMAIN.Dtos.Model;

/// <summary>
/// Classe de informação de chaves revogadas.
/// </summary>C
/// <param name="Id"></param>
/// <param name="RevokedReason"></param>
public record RevokedKeyInfo(
    string Id, string RevokedReason = default)
{
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; } = Id;

    /// <summary>
    /// Razão de revogar.
    /// </summary>
    public string RevokedReason { get; } = RevokedReason;
}