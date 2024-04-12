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

    /// <summary>
    /// Transforma Tenant config em tenant config response.
    /// </summary>
    /// <param name="tenantConfigurationEntity"></param>
    /// <returns></returns>
    public static TenantConfigurationResponse ToResponse(this TenantConfigurationEntity tenantConfigurationEntity, bool includeTenant = false)
        => new()
        {
            Id = tenantConfigurationEntity.Id,
            ApiKey = tenantConfigurationEntity.ApiKey,
            Created = tenantConfigurationEntity.Created,
            TenantId = tenantConfigurationEntity.TenantId,
            Tenant = includeTenant ? tenantConfigurationEntity.Tenant?.ToResponse() : null,
            Status = tenantConfigurationEntity.Status,
            Updated = tenantConfigurationEntity.Updated,
            TenantIdentityConfiguration = tenantConfigurationEntity.TenantIdentityConfiguration?.ToResponse()
        };


    /// <summary>
    /// Transforma um tenantIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="tenantIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static TenantIdentityConfigurationResponse ToResponse(this TenantIdentityConfigurationEntity tenantIdentityConfigurationEntity, bool includeTenantConfiguration = false)
        => new()
        {
            Id = tenantIdentityConfigurationEntity.Id,
            Created = tenantIdentityConfigurationEntity.Created,
            Updated = tenantIdentityConfigurationEntity.Updated,
            Status = tenantIdentityConfigurationEntity.Status,
            TenantConfigurationId = tenantIdentityConfigurationEntity.TenantConfigurationId,
            TenantConfiguration = includeTenantConfiguration ? tenantIdentityConfigurationEntity.TenantConfiguration?.ToResponse() : null,
            UserIdentityConfiguration = tenantIdentityConfigurationEntity?.UserIdentityConfiguration?.ToResponse()
        };

    /// <summary>
    /// Transforma um UserIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="userIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static UserIdentityConfigurationResponse ToResponse(this UserIdentityConfigurationEntity userIdentityConfigurationEntity)
        => new()
        {
            Id = userIdentityConfigurationEntity.Id,
            AllowedUserNameCharacters = userIdentityConfigurationEntity.AllowedUserNameCharacters,
            Created = userIdentityConfigurationEntity.Created,
            Updated = userIdentityConfigurationEntity.Updated,
            RequireUniqueEmail = userIdentityConfigurationEntity.RequireUniqueEmail,
            Status = userIdentityConfigurationEntity.Status,
            TenantIdentityConfiguration = userIdentityConfigurationEntity?.TenantIdentityConfiguration?.ToResponse()
        };
}
