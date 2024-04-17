using AUTHIO.APPLICATION.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.APPLICATION.Infra.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de LockoutIdentityConfigurationEntity.
/// </summary>
public class LockoutIdentityConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<LockoutIdentityConfigurationEntity>
{
    /// <summary>
    /// Configura a Entidade de Lockout configurations.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<LockoutIdentityConfigurationEntity> builder)
    {
        // Mapeia para a tabela LockoutIdentityConfiguration
        builder.ToTable("LockoutIdentityConfigurations");
    }
}
