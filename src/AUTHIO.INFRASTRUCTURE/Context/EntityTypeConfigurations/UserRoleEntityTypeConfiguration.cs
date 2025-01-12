using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de IdentityUserRole<Guid>.
/// </summary>
public class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    /// <summary>
    /// Configura a Entidade de user role.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        // Define a chave composta com UserId e RoleId
        builder.HasKey(e => new { e.UserId, e.RoleId });

        // Nome da tabela
        builder.ToTable("UserRoles");

        // Relacionamento opcional com as tabelas de Usuário e Role, se necessário
        builder.HasOne<UserEntity>()
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .IsRequired();

        builder.HasOne<RoleEntity>()
               .WithMany()
               .HasForeignKey(e => e.RoleId)
               .IsRequired();
    }
}
