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
    /// Transforma created request para entity.
    /// </summary>
    /// <param name="tenantProvisionRequest"></param>
    /// <returns></returns>
    public static TenantEntity ToEntity(this CreateTenantRequest createTenantRequest, Guid userId)
        => new()
        {
            Name = createTenantRequest.Name,
            Description = createTenantRequest.Description,
            Status = Status.Ativo,
            Created = DateTime.Now,
            UserId = userId,
        };

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
