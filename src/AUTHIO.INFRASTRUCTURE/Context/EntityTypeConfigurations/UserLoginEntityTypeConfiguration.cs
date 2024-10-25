using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de IdentityUserLogin<Guid>.
/// </summary>
public class UserLoginEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    /// <summary>
    /// Configura a Entidade de user login.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        // Define a chave composta usando LoginProvider e ProviderKey
        builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });

        // Nome da tabela
        builder.ToTable("UserLogins");

        // Configura o tamanho máximo das colunas
        builder.Property(e => e.LoginProvider).HasMaxLength(128);
        builder.Property(e => e.ProviderKey).HasMaxLength(128);

        // Relacionamento opcional com a tabela de usuários
        builder.HasOne<UserEntity>()
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .IsRequired();
    }
}
