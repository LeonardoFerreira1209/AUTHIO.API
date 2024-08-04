using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de UserIdentityConfigurationEntity.
/// </summary>
public class UserIdentityConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<UserIdentityConfigurationEntity>
{
    /// <summary>
    /// Configura a Entidade de usuários.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<UserIdentityConfigurationEntity> builder)
    {
        // Mapeia para a tabela UserIdentityConfiguration
        builder.ToTable("UserIdentityConfigurations");
    }
}
