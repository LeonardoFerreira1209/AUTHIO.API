using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// interface base de Entidade.
/// </summary>
public interface IEntityBase
{
    /// <summary>
    /// Chave primaria.
    /// </summary>
    public Guid Id { get; }

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
