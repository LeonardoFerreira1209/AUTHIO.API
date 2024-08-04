using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de PasswordIdentityConfigurationEntity.
/// </summary>
public class PasswordIdentityConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<PasswordIdentityConfigurationEntity>
{
    /// <summary>
    /// Configura a Entidade de Password Identity Configuration.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<PasswordIdentityConfigurationEntity> builder)
    {
        // Mapeia para a tabela PasswordIdentityConfiguration
        builder.ToTable("PasswordIdentityConfigurations");
    }
}
