using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de IdentityUserClaim<Guid>.
/// </summary>
public class UserClaimEntityTypeConfiguration : IEntityTypeConfiguration<UserClaimEntity>
{
    /// <summary>
    /// Configura a Entidade de user claim.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(e => e.Id);

        // Nome da tabela
        builder.ToTable("UserClaims");

        // Configura o tamanho das colunas, se necessário
        builder.Property(e => e.ClaimType).HasMaxLength(256);
        builder.Property(e => e.ClaimValue).HasMaxLength(1024);

        // Relacionamento opcional com a tabela de usuários
        builder.HasOne<UserEntity>()
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .IsRequired();
    }
}
