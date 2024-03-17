using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// Feature Flags entity
/// </summary>
public class FeatureFlagsEntity : IEntityBase
{
    /// <summary>
    /// Id da Flag.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; } = null;

    /// <summary>
    /// Status do cadastro.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Nome
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Está ativa
    /// </summary>
    public bool IsEnabled { get; set; }
}
