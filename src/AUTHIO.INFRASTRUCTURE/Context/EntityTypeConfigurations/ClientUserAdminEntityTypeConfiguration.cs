using AUTHIO.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;

/// <summary>
/// Configuração de modelo de ClientUserAdminEntity.
/// </summary>
public class ClientUserAdminEntityTypeConfiguration : IEntityTypeConfiguration<ClientIdentityUserAdminEntity>
{
    /// <summary>
    /// Configura a Entidade de Client user admin.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(
        EntityTypeBuilder<ClientIdentityUserAdminEntity> builder)
    {
        builder.ToTable("ClientUserAdmins");

        // Configura uma chave primária composta para a entidade, usando os campos UserId
        // e ClientId. Isso significa que cada combinação de UserId e ClientId deve ser única no conjunto de dados.
        builder.HasKey(tua => new { tua.UserId, tua.ClientId });

        // Configura uma relação de um-para-muitos entre User e a entidade atual, indicando que um User pode estar associado a muitos registros desta entidade.
        // A chave estrangeira UserId na entidade atual é usada para estabelecer esta relação. Não é especificada uma propriedade de navegação na entidade User, indicando que a relação é unidirecional.
        builder
            .HasOne(tua => tua.User)
            .WithMany()
            .HasForeignKey(tua => tua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configura uma relação de muitos-para-um entre Client e a entidade atual, onde cada instância desta entidade (a entidade atual) está associada a um Client.
        // A propriedade ClientId na entidade atual é usada como chave estrangeira.
        // Além disso, especifica que um Client pode estar associado a muitos UserAdmins, utilizando a propriedade UserAdmins na entidade Client para representar o lado "muitos" da relação.
        builder
            .HasOne(tua => tua.Client)
            .WithMany(t => t.UserAdmins)
            .HasForeignKey(tua => tua.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
