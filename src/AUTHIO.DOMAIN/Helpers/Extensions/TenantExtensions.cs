﻿using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

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
        bool includeTenant = false, bool includeTenantIdentityConfiguration = true, bool includeTenantEmailConfiguration = true, bool includeTenantTokenConfiguration = true)
        => new()
        {
            Id = tenantConfigurationEntity.Id,
            TenantId = tenantConfigurationEntity.TenantId,
            Created = tenantConfigurationEntity.Created,
            Updated = tenantConfigurationEntity.Updated,
            TenantKey = tenantConfigurationEntity.TenantKey,

            TenantIdentityConfiguration = includeTenantIdentityConfiguration
                ? tenantConfigurationEntity.TenantIdentityConfiguration?.ToResponse()
                : null,

            TenantEmailConfiguration = includeTenantEmailConfiguration 
                ? tenantConfigurationEntity.TenantEmailConfiguration?.ToResponse() 
                : null,

            TenantTokenConfiguration = includeTenantTokenConfiguration
                ? tenantConfigurationEntity.TenantTokenConfiguration?.ToResponse()
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
        bool includeTenantConfiguration = false, bool includeUserIdentityConfiguration = true,
        bool includePasswordIdentityConfiguration = true, bool includeLockoutIdentityConfiguration = true)
        => new()
        {
            Id = tenantIdentityConfigurationEntity.Id,
            TenantConfigurationId = tenantIdentityConfigurationEntity.TenantConfigurationId,
            Created = tenantIdentityConfigurationEntity.Created,
            Updated = tenantIdentityConfigurationEntity.Updated,

            UserIdentityConfiguration = includeUserIdentityConfiguration
                ? tenantIdentityConfigurationEntity?.UserIdentityConfiguration?.ToResponse()
                : null,

            PasswordIdentityConfiguration = includePasswordIdentityConfiguration
                ? tenantIdentityConfigurationEntity?.PasswordIdentityConfiguration?.ToResponse()
                : null,

            LockoutIdentityConfiguration = includeLockoutIdentityConfiguration
                 ? tenantIdentityConfigurationEntity.LockoutIdentityConfiguration?.ToResponse()
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

    /// <summary>
    /// Transforma um PasswordIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="passwordIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static PasswordIdentityConfigurationResponse ToResponse(this PasswordIdentityConfigurationEntity passwordIdentityConfigurationEntity,
        bool includeTenantIdentityConfiguration = false)
        => new()
        {
            Id = passwordIdentityConfigurationEntity.Id,
            Created = passwordIdentityConfigurationEntity.Created,
            Updated = passwordIdentityConfigurationEntity.Updated,
            RequireDigit = passwordIdentityConfigurationEntity.RequireDigit,
            RequiredLength = passwordIdentityConfigurationEntity.RequiredLength,
            RequiredUniqueChars = passwordIdentityConfigurationEntity.RequiredUniqueChars,
            RequireLowercase = passwordIdentityConfigurationEntity.RequireLowercase,
            RequireNonAlphanumeric = passwordIdentityConfigurationEntity.
            RequireNonAlphanumeric = passwordIdentityConfigurationEntity.RequireNonAlphanumeric,
            TenantIdentityConfigurationId = passwordIdentityConfigurationEntity.TenantIdentityConfigurationId,
            TenantIdentityConfiguration = includeTenantIdentityConfiguration
                ? passwordIdentityConfigurationEntity?.TenantIdentityConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um LockoutIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="lockoutIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static LockoutIdentityConfigurationResponse ToResponse(this LockoutIdentityConfigurationEntity lockoutIdentityConfigurationEntity,
        bool includeTenantIdentityConfiguration = false)
        => new()
        {
            Id = lockoutIdentityConfigurationEntity.Id,
            Created = lockoutIdentityConfigurationEntity.Created,
            Updated = lockoutIdentityConfigurationEntity.Updated,
            AllowedForNewUsers = lockoutIdentityConfigurationEntity.AllowedForNewUsers,
            DefaultLockoutTimeSpan = lockoutIdentityConfigurationEntity.DefaultLockoutTimeSpan,
            MaxFailedAccessAttempts = lockoutIdentityConfigurationEntity.MaxFailedAccessAttempts,
            TenantIdentityConfigurationId = lockoutIdentityConfigurationEntity.TenantIdentityConfigurationId,
            TenantIdentityConfiguration = includeTenantIdentityConfiguration
                ? lockoutIdentityConfigurationEntity?.TenantIdentityConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um TenantEmailConfigurationEntity em response.
    /// </summary>
    /// <param name="TenantEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static TenantEmailConfigurationResponse ToResponse(this TenantEmailConfigurationEntity tenantEmailConfigurationEntity,
        bool includeTenantConfiguration = false)
        => new()
        {
            Id = tenantEmailConfigurationEntity.Id,
            Created = tenantEmailConfigurationEntity.Created,
            Updated = tenantEmailConfigurationEntity.Updated,
            SendersName = tenantEmailConfigurationEntity.SendersName,
            SendersEmail = tenantEmailConfigurationEntity.SendersEmail,
            IsEmailConfirmed = tenantEmailConfigurationEntity.IsEmailConfirmed,
            TenantConfigurationId = tenantEmailConfigurationEntity.TenantConfigurationId,
            SendGridConfiguration = tenantEmailConfigurationEntity?.SendGridConfiguration?.ToResponse(false),
            TenantConfiguration = includeTenantConfiguration
                ? tenantEmailConfigurationEntity?.TenantConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um SendGridConfigurationEntity em response.
    /// </summary>
    /// <param name="sendGridConfigurationEntity"></param>
    /// <param name="includeTenantEmailConfiguration"></param>
    /// <returns></returns>
    public static SendGridConfigurationResponse ToResponse(this SendGridConfigurationEntity sendGridConfigurationEntity,
        bool includeTenantEmailConfiguration)
        => new()
        {
            Id = sendGridConfigurationEntity.Id,
            Created = sendGridConfigurationEntity.Created,
            Updated = sendGridConfigurationEntity.Updated,
            SendGridApiKey = sendGridConfigurationEntity.SendGridApiKey,
            WelcomeTemplateId = sendGridConfigurationEntity.WelcomeTemplateId,
            TenantEmailConfigurationId = sendGridConfigurationEntity.TenantEmailConfigurationId,
            TenantEmailConfiguration = includeTenantEmailConfiguration
                ? sendGridConfigurationEntity?.TenantEmailConfiguration?.ToResponse()
                : null
        };
  
    /// <summary>
    /// Transforma um TenantTokenConfigurationEntity em response.
    /// </summary>
    /// <param name="tenantTokenConfigurationEntity"></param>
    /// <returns>TenantTokenConfigurationResponse</returns>
    public static TenantTokenConfigurationResponse ToResponse(this TenantTokenConfigurationEntity tenantTokenConfigurationEntity,
        bool includeTenantConfiguration = false)
        => new()
        {
            Id = tenantTokenConfigurationEntity.Id,
            Created = tenantTokenConfigurationEntity.Created,
            Updated = tenantTokenConfigurationEntity.Updated,
            SecurityKey = tenantTokenConfigurationEntity.SecurityKey,
            Issuer = tenantTokenConfigurationEntity.Issuer,
            Audience = tenantTokenConfigurationEntity.Audience,
            TenantConfigurationId = tenantTokenConfigurationEntity.TenantConfigurationId,
            TenantConfiguration = includeTenantConfiguration
                ? tenantTokenConfigurationEntity?.TenantConfiguration?.ToResponse()
                : null
        };
}
