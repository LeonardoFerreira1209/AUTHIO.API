namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Interface de Client.
/// </summary>
public interface IEntityClient
{
    /// <summary>
    /// Id do Client.
    /// </summary>
    public Guid ClientId { get; }
}
