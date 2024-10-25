using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de IdentityRoleClaim<Guid>.
/// </summary>
public class RoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    /// <summary>
    /// Configura a Entidade de role claim.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        // Nome da tabela
        builder.ToTable("RoleClaims");
    }
}
