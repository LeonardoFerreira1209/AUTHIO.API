using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de ClientEntity.
/// </summary>
public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    /// <summary>
    /// Configura a Entidade de Client.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<ClientEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(t => t.Id);

        // Define o limite máximo de caracteres para o nome para utilizar tipos de banco de dados eficientes
        builder.Property(t => t.Name).HasMaxLength(256);

        // Define o limite máximo de caracteres para a descrição para utilizar tipos de banco de dados eficientes
        builder.Property(t => t.Description).HasMaxLength(512);

        // Configura uma relação um-para-um entre Client e ClientConfiguration. Especifica que cada Client tem uma ClientConfiguration,
        // e cada ClientConfiguration está associada a exatamente um Client, usando ClientId como chave estrangeira.
        builder
            .HasOne(t => t.ClientConfiguration)
            .WithOne(tc => tc.Client)
            .HasForeignKey<ClientConfigurationEntity>(tc => tc.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configura a relação de muitos para um entre Clients e usuarios.
        builder
            .HasMany(t => t.Users)
            .WithOne(tc => tc.Client)
            .HasForeignKey(tc => tc.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
