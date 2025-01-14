using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de RealmEntity.
/// </summary>
public class RealmIdentityConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<RealmEntity>
{
    /// <summary>
    /// Configura a Entidade de Realm.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<RealmEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(t => t.Id);

        // Configura a relação de muitos para um entre Realms e clients.
        builder
            .HasMany(t => t.Clients)
            .WithOne(tc => tc.Realm)
            .HasForeignKey(tc => tc.RealmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
