using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// interface base de Entidade.
/// </summary>
public interface IEntityBase : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; }

    /// <summary>
    /// Status do cadastro.
    /// </summary>
    public Status Status { get; }
}
