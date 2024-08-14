using AUTHIO.DOMAIN.Dtos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de KeyMaterial.
/// </summary>
public class KeyMaterialEntityTypeConfiguration : IEntityTypeConfiguration<KeyMaterial>
{
    /// <summary>
    /// Configura a Entidade de KeyMaterial.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<KeyMaterial> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Parameters)
            .HasMaxLength(8000)
            .IsRequired();
    }
}
