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
    public static TenantResponse ToResponse(this TenantEntity tenantEntity, 
        bool includeUsers = true, bool includeUserAdmins = true, bool includeTenantConfig = true) 
        => new() 
        {
            Id = tenantEntity.Id,
            Created = tenantEntity.Created,
            Updated = tenantEntity.Updated,
            Name = tenantEntity.Name,
            Description = tenantEntity.Description,
            Status = tenantEntity.Status,

            UserAdmins = includeUserAdmins 
                ? tenantEntity?.UserAdmins?.Select(user => new TenantUserAdminResponse { TenantId = user.TenantId, UserId = user.UserId }).ToList() 
                : [],

            Users = includeUsers 
                ? tenantEntity?.Users?.Select(user => user?.ToResponse()).ToList() 
                : [],

            TenantConfiguration = includeTenantConfig 
                ? tenantEntity?.TenantConfiguration?.ToResponse() 
                : null
        };

    /// <summary>
    /// Transforma Tenant config em tenant config response.
    /// </summary>
    /// <param name="tenantConfigurationEntity"></param>
    /// <returns></returns>
    public static TenantConfigurationResponse ToResponse(this TenantConfigurationEntity tenantConfigurationEntity,
        bool includeTenant = false, bool includeTenantIdentityConfiguration = true)
        => new()
        {
            Id = tenantConfigurationEntity.Id,
            TenantId = tenantConfigurationEntity.TenantId,
            Created = tenantConfigurationEntity.Created,
            Updated = tenantConfigurationEntity.Updated,
            ApiKey = tenantConfigurationEntity.ApiKey,

            TenantIdentityConfiguration = includeTenantIdentityConfiguration
                ? tenantConfigurationEntity.TenantIdentityConfiguration?.ToResponse() 
                : null,

            Tenant = includeTenant 
                ? tenantConfigurationEntity.Tenant?.ToResponse() 
                : null
        };

    /// <summary>
    /// Transforma um tenantIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="tenantIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static TenantIdentityConfigurationResponse ToResponse(this TenantIdentityConfigurationEntity tenantIdentityConfigurationEntity, 
        bool includeTenantConfiguration = false, bool includeUserIdentityConfiguration = true)
        => new()
        {
            Id = tenantIdentityConfigurationEntity.Id,
            TenantConfigurationId = tenantIdentityConfigurationEntity.TenantConfigurationId,
            Created = tenantIdentityConfigurationEntity.Created,
            Updated = tenantIdentityConfigurationEntity.Updated,
            
            UserIdentityConfiguration = includeUserIdentityConfiguration 
                ?  tenantIdentityConfigurationEntity?.UserIdentityConfiguration?.ToResponse() 
                : null,

            TenantConfiguration = includeTenantConfiguration
                ? tenantIdentityConfigurationEntity.TenantConfiguration?.ToResponse() 
                : null
        };

    /// <summary>
    /// Transforma um UserIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="userIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static UserIdentityConfigurationResponse ToResponse(this UserIdentityConfigurationEntity userIdentityConfigurationEntity, 
        bool includeTenantIdentityConfiguration = false)
        => new()
        {
            Id = userIdentityConfigurationEntity.Id,
            Created = userIdentityConfigurationEntity.Created,
            Updated = userIdentityConfigurationEntity.Updated,
            AllowedUserNameCharacters = userIdentityConfigurationEntity.AllowedUserNameCharacters,
            RequireUniqueEmail = userIdentityConfigurationEntity.RequireUniqueEmail,
            TenantIdentityConfigurationId = userIdentityConfigurationEntity.TenantIdentityConfigurationId,
            TenantIdentityConfiguration = includeTenantIdentityConfiguration
                ? userIdentityConfigurationEntity?.TenantIdentityConfiguration?.ToResponse() 
                : null
        };
}
