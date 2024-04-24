using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.DATABASE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de TenantUserAdminEntity.
/// </summary>
public class TenantUserAdminEntityTypeConfiguration : IEntityTypeConfiguration<TenantIdentityUserAdminEntity>
{
    /// <summary>
    /// Configura a Entidade de tenant user admin.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<TenantIdentityUserAdminEntity> builder)
    {
        builder.ToTable("TenantUserAdmins");

        // Configura uma chave primária composta para a entidade, usando os campos UserId
        // e TenantId. Isso significa que cada combinação de UserId e TenantId deve ser única no conjunto de dados.
        builder.HasKey(tua => new { tua.UserId, tua.TenantId });

        // Configura uma relação de um-para-muitos entre User e a entidade atual, indicando que um User pode estar associado a muitos registros desta entidade.
        // A chave estrangeira UserId na entidade atual é usada para estabelecer esta relação. Não é especificada uma propriedade de navegação na entidade User, indicando que a relação é unidirecional.
        builder.HasOne(tua => tua.User).WithMany().HasForeignKey(tua => tua.UserId);

        // Configura uma relação de muitos-para-um entre Tenant e a entidade atual, onde cada instância desta entidade (a entidade atual) está associada a um Tenant.
        // A propriedade TenantId na entidade atual é usada como chave estrangeira.
        // Além disso, especifica que um Tenant pode estar associado a muitos UserAdmins, utilizando a propriedade UserAdmins na entidade Tenant para representar o lado "muitos" da relação.
        builder.HasOne(tua => tua.Tenant).WithMany(t => t.UserAdmins).HasForeignKey(tua => tua.TenantId);
    }
}
