namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Interface de Chave primária.
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntityPrimaryKey<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Chave primária.
    /// </summary>
    public TKey Id { get; set; }
}
