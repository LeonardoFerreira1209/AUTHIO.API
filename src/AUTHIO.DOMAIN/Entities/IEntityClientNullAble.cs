namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Interface de Client nullable.
/// </summary>
public interface IEntityClientNullAble
{
    /// <summary>
    /// Id do Client.
    /// </summary>
    public Guid? ClientId { get; }
}
