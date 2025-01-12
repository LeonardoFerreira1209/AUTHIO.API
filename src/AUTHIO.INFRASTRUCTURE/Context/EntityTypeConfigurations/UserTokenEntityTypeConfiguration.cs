using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de IdentityUserToken<Guid>.
/// </summary>
public class UserTokenEntityTypeConfiguration : IEntityTypeConfiguration<UserTokenEntity>
{
    /// <summary>
    /// Configura a Entidade de user token.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
    {
        // Define a chave composta
        builder.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

        // Nome da tabela
        builder.ToTable("UserTokens");

        // Configura o tamanho das colunas
        builder.Property(e => e.LoginProvider).HasMaxLength(128);
        builder.Property(e => e.Name).HasMaxLength(128);
    }
}
