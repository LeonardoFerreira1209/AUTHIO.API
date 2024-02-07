using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Utils.Extensions;

/// <summary>
/// Classe de extensão de Tenants.
/// </summary>
public static class TenantExtensions
{
    /// <summary>
    /// Transforma Provision request to entities.
    /// </summary>
    /// <param name="tenantProvisionRequest"></param>
    /// <returns></returns>
    public static (TenantEntity tenant, UserEntity user) ToEntities(this TenantProvisionRequest tenantProvisionRequest)
        => (new ()
        {
            Created = DateTime.Now,
            Name = tenantProvisionRequest.Name,
            Description = tenantProvisionRequest.Description,
            Status = Status.Ativo

        }, new ()
        {
            Created = DateTime.Now,
            UserName = tenantProvisionRequest.UserAdmin.Username,
            Name = tenantProvisionRequest.UserAdmin.Name,
            LastName = tenantProvisionRequest.UserAdmin.LastName,
            PhoneNumber = tenantProvisionRequest.UserAdmin.PhoneNumber,
            Email = tenantProvisionRequest.UserAdmin.Email,
            Status = Status.Ativo
        });

    public static TenantResponse ToResponse(this TenantEntity tenantEntity) 
        => new() 
        {
            Id = tenantEntity.Id,
            Created = tenantEntity.Created,
            Updated = tenantEntity.Updated,
            Name = tenantEntity.Name,
            Description = tenantEntity.Description,
            Status = tenantEntity.Status,
            UserAdmins = tenantEntity?.UserAdmins.Select(userA => new TenantUserAdminResponse {
                TenantId = userA.TenantId,
                UserId = userA.TenantId,
            }).ToList(),
            Users = tenantEntity?.Users.Select(user => user?.ToResponse()).ToList(),
        };
}
