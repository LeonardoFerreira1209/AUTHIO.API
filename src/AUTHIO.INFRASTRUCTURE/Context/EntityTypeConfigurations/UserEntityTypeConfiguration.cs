using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de UserEntity.
/// </summary>
public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
{
    /// <summary>
    /// Configura a Entidade de usuários.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<UserEntity> builder)
    {
        // Chave primária
        builder.HasKey(u => u.Id);

        // Índices para o nome de usuário e e-mail "normalizados", para permitir buscas eficientes
        builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique(false);
        builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

        // Mapeia para a tabela AspNetUsers
        builder.ToTable("AspNetUsers");

        // Um token de concorrência para uso com a verificação de concorrência otimista
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limita o tamanho das colunas para usar tipos de banco de dados eficientes
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

        // Relacionamento de usuário com assinatura.
        builder
           .HasOne(u => u.Subscription)  
           .WithOne(s => s.User)      
           .HasForeignKey<SubscriptionEntity>(s => s.UserId);

        // Cada Usuário pode ter muitos UserClaims
        builder
            .HasMany<IdentityUserClaim<Guid>>()
            .WithOne()
            .HasForeignKey(uc => uc.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Cada Usuário pode ter muitos UserLogins
        builder
            .HasMany<IdentityUserLogin<Guid>>()
            .WithOne()
            .HasForeignKey(ul => ul.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Cada Usuário pode ter muitos UserTokens
        builder
            .HasMany<IdentityUserLogin<Guid>>()
            .WithOne()
            .HasForeignKey(ut => ut.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Cada Usuário pode ter muitas entradas na tabela de junção UserRole
        builder
            .HasMany<IdentityUserRole<Guid>>()
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // Define a relação entre o Usuário e a entidade Tenant. Cada Usuário pertence a um Tenant,
        // e cada Tenant pode ter vários Usuários. Esta relação é configurada para não permitir a exclusão em cascata.
        builder
            .HasOne(u => u.Tenant)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
