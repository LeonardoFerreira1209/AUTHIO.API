using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.DATABASE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de TenantEntity.
/// </summary>
public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<TenantEntity>
{
    /// <summary>
    /// Configura a Entidade de tenant.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<TenantEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(t => t.Id);

        // Define o limite máximo de caracteres para o nome para utilizar tipos de banco de dados eficientes
        builder.Property(t => t.Name).HasMaxLength(256);

        // Define o limite máximo de caracteres para a descrição para utilizar tipos de banco de dados eficientes
        builder.Property(t => t.Description).HasMaxLength(512);

        // Configura uma relação um-para-um entre Tenant e TenantConfiguration. Especifica que cada Tenant tem uma TenantConfiguration,
        // e cada TenantConfiguration está associada a exatamente um Tenant, usando TenantId como chave estrangeira.
        builder.HasOne(t => t.TenantConfiguration).WithOne(tc => tc.Tenant).HasForeignKey<TenantConfigurationEntity>(tc => tc.TenantId);
    }
}
