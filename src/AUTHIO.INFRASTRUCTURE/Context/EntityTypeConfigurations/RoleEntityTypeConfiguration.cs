using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de RoleEntity.
/// </summary>
public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    /// <summary>
    /// Configura a Entidade de roles.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        // Chave primária
        builder.HasKey(r => r.Id);

        // Índice para o nome de Role "normalizado" para permitir buscas eficientes
        builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

        // Mapeia para a tabela AspNetRoles
        builder.ToTable("AspNetRoles");

        // Um token de concorrência para uso com a verificação de concorrência otimista
        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

        // Limita o tamanho das colunas para usar tipos de banco de dados eficientes
        builder.Property(r => r.Name).HasMaxLength(256);
        builder.Property(r => r.NormalizedName).HasMaxLength(256);

        // As relações entre Role e outros tipos de entidade
        // Observe que essas relações são configuradas sem propriedades de navegação

        // Cada Role pode ter muitas entradas na tabela de junção UserRole
        builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

        // Cada Role pode ter muitas entradas na tabela de junção UserRole. Esta configuração especifica que o relacionamento entre Role e UserRole é de um-para-muitos,
        // onde cada Role pode estar associado a muitos UserRole. A chave estrangeira RoleId em UserRole é usada para estabelecer este relacionamento e é marcada como obrigatória.
        builder.HasMany<IdentityRoleClaim<Guid>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

        // Define a relação entre o Role e a entidade Tenant. Cada Role pertence a um Tenant,
        // e cada Tenant pode ter vários Roles. Esta relação é configurada para não permitir a exclusão em cascata.
        builder.HasOne(x => x.Tenant).WithMany(t => t.Roles).HasForeignKey(x => x.TenantId).OnDelete(DeleteBehavior.Restrict);
    }
}
