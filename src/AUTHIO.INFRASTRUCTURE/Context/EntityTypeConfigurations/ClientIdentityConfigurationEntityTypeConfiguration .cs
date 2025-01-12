using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de ClientIdentityConfigurationEntity.
/// </summary>
public class ClientIdentityConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ClientIdentityConfigurationEntity>
{
    /// <summary>
    /// Configura a Entidade de Client Identity Configuration.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<ClientIdentityConfigurationEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(t => t.Id);

        // Configura uma relação um-para-um entre UserIdentityConfiguration e ClientIdentityConfiguration. Especifica que cada UserIdentityConfiguration tem uma ClientIdentityConfiguration,
        // e cada ClientIdentityConfiguration está associada a exatamente um UserIdentityConfiguration, usando UserIdentityConfigurationId como chave estrangeira.
        builder
            .HasOne(tic => tic.UserIdentityConfiguration)
            .WithOne(tc => tc.ClientIdentityConfiguration)
            .HasForeignKey<UserIdentityConfigurationEntity>(tc => tc.ClientIdentityConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configura uma relação um-para-um entre PasswordIdentityConfigurationEntity e ClientIdentityConfiguration. Especifica que cada PasswordIdentityConfigurationEntity tem uma ClientIdentityConfiguration,
        // e cada ClientIdentityConfiguration está associada a exatamente um PasswordIdentityConfigurationEntity, usando PasswordIdentityConfigurationEntity como chave estrangeira.
        builder
            .HasOne(tic => tic.PasswordIdentityConfiguration)
            .WithOne(tc => tc.ClientIdentityConfiguration)
            .HasForeignKey<PasswordIdentityConfigurationEntity>(tc => tc.ClientIdentityConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
