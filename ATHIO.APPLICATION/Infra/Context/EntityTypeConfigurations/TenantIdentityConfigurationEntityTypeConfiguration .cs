using AUTHIO.APPLICATION.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.APPLICATION.Infra.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de TenantIdentityConfigurationEntity.
/// </summary>
public class TenantIdentityConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<TenantIdentityConfigurationEntity>
{
    /// <summary>
    /// Configura a Entidade de Tenant Identity Configuration.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<TenantIdentityConfigurationEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(t => t.Id);

        // Configura uma relação um-para-um entre UserIdentityConfiguration e TenantIdentityConfiguration. Especifica que cada UserIdentityConfiguration tem uma TenantIdentityConfiguration,
        // e cada TenantIdentityConfiguration está associada a exatamente um UserIdentityConfiguration, usando UserIdentityConfigurationId como chave estrangeira.
        builder.HasOne(tic => tic.UserIdentityConfiguration).WithOne(tc => tc.TenantIdentityConfiguration).HasForeignKey<UserIdentityConfigurationEntity>(tc => tc.TenantIdentityConfigurationId);

        // Configura uma relação um-para-um entre PasswordIdentityConfigurationEntity e TenantIdentityConfiguration. Especifica que cada PasswordIdentityConfigurationEntity tem uma TenantIdentityConfiguration,
        // e cada TenantIdentityConfiguration está associada a exatamente um PasswordIdentityConfigurationEntity, usando PasswordIdentityConfigurationEntity como chave estrangeira.
        builder.HasOne(tic => tic.PasswordIdentityConfiguration).WithOne(tc => tc.TenantIdentityConfiguration).HasForeignKey<PasswordIdentityConfigurationEntity>(tc => tc.TenantIdentityConfigurationId);
    }
}
