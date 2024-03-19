using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Entities;
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
            UserId = userId
        };

    /// <summary>
    /// Transforma Tenant config em tenant config response.
    /// </summary>
    /// <param name="tenantConfigurationEntity"></param>
    /// <returns></returns>
    public static TenantConfigurationResponse ToResponse(this TenantConfigurationEntity tenantConfigurationEntity)
        => new()
        {
            ApiKey = tenantConfigurationEntity.ApiKey,
            Created = tenantConfigurationEntity.Created,
            TenantId = tenantConfigurationEntity.TenantId,
            Id = tenantConfigurationEntity.Id,
            Status = tenantConfigurationEntity.Status,
            Updated = tenantConfigurationEntity.Updated
        };

    /// <summary>
    /// Transforma um Tenant Entity em response.
    /// </summary>
    /// <param name="tenantEntity"></param>
    /// <returns></returns>
    public static TenantResponse ToResponse(this TenantEntity tenantEntity) 
        => new() 
        {
            Id = tenantEntity.Id,
            Created = tenantEntity.Created,
            Updated = tenantEntity.Updated,
            Name = tenantEntity.Name,
            Description = tenantEntity.Description,
            Status = tenantEntity.Status,
            UserAdmins = tenantEntity?.UserAdmins?.Select(user => new TenantUserAdminResponse {
                TenantId = user.TenantId,
                UserId = user.UserId,
            }).ToList(),
            Users = tenantEntity?.Users?.Select(user => user?.ToResponse()).ToList(),
            TenantConfiguration = tenantEntity?.TenantConfiguration?.ToResponse()
        };
}
