using AUTHIO.DOMAIN.Dtos.Model;
using Microsoft.EntityFrameworkCore;

namespace AUTHIO.DOMAIN.Store;

/// <summary>
/// Interface de Security Key Context.
/// </summary>
public interface ISecurityKeyContext
{
    /// <summary>
    /// Chaves.
    /// </summary>
    DbSet<KeyMaterial> SecurityKeys { get; set; }
}
