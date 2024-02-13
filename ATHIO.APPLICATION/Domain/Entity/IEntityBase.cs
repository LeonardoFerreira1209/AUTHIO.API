using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// interface base de Entidade.
/// </summary>
public interface IEntityBase
{
    /// <summary>
    /// Chave primaria.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Status do cadastro.
    /// </summary>
    public Status Status { get; set; }
}
