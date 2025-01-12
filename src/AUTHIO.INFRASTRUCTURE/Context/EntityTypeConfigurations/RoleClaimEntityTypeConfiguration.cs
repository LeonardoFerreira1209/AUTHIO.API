using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de IdentityRoleClaim<Guid>.
/// </summary>
public class RoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<RoleClaimEntity>
{
    /// <summary>
    /// Configura a Entidade de role claim.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<RoleClaimEntity> builder)
    {
        // Nome da tabela
        builder.ToTable("RoleClaims");
    }
}
