using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de extensão de Clients.
/// </summary>
public static class ClientExtensions
{
    /// <summary>
    /// Transforma created request para entity.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static ClientEntity ToEntity(
        this CreateClientRequest createClientRequest, 
        Guid userId, 
        Guid realmId
        )
        => CreateClient.CreateDefault(
            userId,
            realmId,
            createClientRequest.Name,
            createClientRequest.Description,
            CreateClientConfiguration.CreateDefault(
                Guid.Empty,
                CreateClientIdentityConfiguration.CreateDefault(
                    Guid.Empty,
                    CreateUserIdentityConfiguration.CreateDefault(Guid.Empty),
                    CreatePasswordIdentityConfiguration.CreateDefault(Guid.Empty),
                    CreateLockoutIdentityConfiguration.CreateDefault(Guid.Empty)
                ),
                CreateClientEmailConfiguration.CreateDefault(
                    Guid.Empty,
                    createClientRequest.Name,
                    createClientRequest.Email,
                    true,
                    CreateSendGridConfiguration.CreateDefault(
                        Guid.Empty,
                        createClientRequest.SendGridApiKey,
                        createClientRequest.WelcomeTemplateId
                    )
                ),
                CreateClientTokenConfiguration.CreateDefault(
                    Guid.Empty,
                    createClientRequest?.TokenConfiguration?.SecurityKey,
                    createClientRequest?.TokenConfiguration?.Issuer,
                    createClientRequest?.TokenConfiguration?.Audience,
                    createClientRequest?.TokenConfiguration.Encrypted ?? false,
                    createClientRequest?.TokenConfiguration.AlgorithmJwsType ?? AlgorithmType.RSA,
                    createClientRequest?.TokenConfiguration.AlgorithmJweType ?? AlgorithmType.RSA
                )
            )
        );

    /// <summary>
    /// Atualiza a entidade de Client.
    /// </summary>
    /// <param name="updateClientRequest"></param>
    /// <param name="clientEntity"></param>
    /// <returns></returns>
    public static ClientEntity UpdateEntity(
        this UpdateClientRequest updateClientRequest, ClientEntity clientEntity)
    {
        clientEntity.Name = updateClientRequest.Name;
        clientEntity.Description = updateClientRequest.Description;
        clientEntity.Updated = DateTime.Now;

        clientEntity.ClientConfiguration 
            = updateClientRequest
                ?.ClientConfiguration
                    ?.UpdateEntity(clientEntity.ClientConfiguration) ?? clientEntity.ClientConfiguration;

        return clientEntity;
    }
    
    /// <summary>
    /// Atualiza a entidade de Client configuration.
    /// </summary>
    /// <param name="updateClientConfigurationRequest"></param>
    /// <param name="ClientConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientConfigurationEntity UpdateEntity(
        this UpdateClientConfigurationRequest updateClientConfigurationRequest, ClientConfigurationEntity clientConfigurationEntity)
    {
        clientConfigurationEntity.Updated = DateTime.Now;

        clientConfigurationEntity.ClientIdentityConfiguration 
            = updateClientConfigurationRequest
                ?.ClientIdentityConfiguration
                    ?.UpdateEntity(clientConfigurationEntity.ClientIdentityConfiguration) 
                        ?? clientConfigurationEntity.ClientIdentityConfiguration;

        clientConfigurationEntity.ClientEmailConfiguration 
            = updateClientConfigurationRequest
                ?.ClientEmailConfiguration
                    ?.UpdateEntity(clientConfigurationEntity.ClientEmailConfiguration) 
                        ?? clientConfigurationEntity.ClientEmailConfiguration;

        clientConfigurationEntity.ClientTokenConfiguration
            = updateClientConfigurationRequest
                ?.ClientTokenConfiguration
                    ?.UpdateEntity(clientConfigurationEntity.ClientTokenConfiguration) 
                        ?? clientConfigurationEntity.ClientTokenConfiguration;

        return clientConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de Client identity configuration.
    /// </summary>
    /// <param name="updateClientIdentityConfigurationRequest"></param>
    /// <param name="clientIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientIdentityConfigurationEntity UpdateEntity(
        this UpdateClientIdentityConfigurationRequest updateClientIdentityConfigurationRequest, ClientIdentityConfigurationEntity clientIdentityConfigurationEntity)
    { 
        clientIdentityConfigurationEntity.Updated = DateTime.Now;

        clientIdentityConfigurationEntity.UserIdentityConfiguration 
            = updateClientIdentityConfigurationRequest
                ?.UserIdentityConfiguration
                    ?.UpdateEntity(clientIdentityConfigurationEntity.UserIdentityConfiguration) 
                        ?? clientIdentityConfigurationEntity.UserIdentityConfiguration;

        clientIdentityConfigurationEntity.PasswordIdentityConfiguration 
            = updateClientIdentityConfigurationRequest
                ?.PasswordIdentityConfiguration
                    ?.UpdateEntity(clientIdentityConfigurationEntity.PasswordIdentityConfiguration) 
                        ?? clientIdentityConfigurationEntity.PasswordIdentityConfiguration;

        clientIdentityConfigurationEntity.LockoutIdentityConfiguration 
            = updateClientIdentityConfigurationRequest
                ?.LockoutIdentityConfiguration
                    ?.UpdateEntity(clientIdentityConfigurationEntity.LockoutIdentityConfiguration) 
                        ?? clientIdentityConfigurationEntity.LockoutIdentityConfiguration;

        return clientIdentityConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de Password Identity configuration.
    /// </summary>
    /// <param name="updatePasswordIdentityConfigurationRequest"></param>
    /// <param name="passwordIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static PasswordIdentityConfigurationEntity UpdateEntity(
        this UpdatePasswordIdentityConfigurationRequest updatePasswordIdentityConfigurationRequest, PasswordIdentityConfigurationEntity passwordIdentityConfigurationEntity)
    {
        passwordIdentityConfigurationEntity.Updated = DateTime.Now;
        passwordIdentityConfigurationEntity.RequireDigit = updatePasswordIdentityConfigurationRequest.RequireDigit;
        passwordIdentityConfigurationEntity.RequiredUniqueChars = updatePasswordIdentityConfigurationRequest.RequiredUniqueChars;
        passwordIdentityConfigurationEntity.RequiredLength = updatePasswordIdentityConfigurationRequest.RequiredLength;
        passwordIdentityConfigurationEntity.RequireUppercase = updatePasswordIdentityConfigurationRequest.RequireUppercase;
        passwordIdentityConfigurationEntity.RequireLowercase = updatePasswordIdentityConfigurationRequest.RequireLowercase;
        passwordIdentityConfigurationEntity.RequireNonAlphanumeric = updatePasswordIdentityConfigurationRequest.RequireNonAlphanumeric;

        return passwordIdentityConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de Client Email Configuration.
    /// </summary>
    /// <param name="updateClientEmailConfigurationRequest"></param>
    /// <param name="clientEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientEmailConfigurationEntity UpdateEntity(
        this UpdateClientEmailConfigurationRequest updateClientEmailConfigurationRequest, ClientEmailConfigurationEntity clientEmailConfigurationEntity)
    {
        clientEmailConfigurationEntity.Updated = DateTime.Now;
        clientEmailConfigurationEntity.SendersName = updateClientEmailConfigurationRequest.SendersName;
        clientEmailConfigurationEntity.SendersEmail = updateClientEmailConfigurationRequest.SendersEmail;
        clientEmailConfigurationEntity.IsEmailConfirmed = updateClientEmailConfigurationRequest.IsEmailConfirmed;

        clientEmailConfigurationEntity.SendGridConfiguration 
            = updateClientEmailConfigurationRequest
                ?.SendGridConfiguration
                    ?.UpdateEntity(clientEmailConfigurationEntity.SendGridConfiguration) 
                        ?? clientEmailConfigurationEntity.SendGridConfiguration;

        return clientEmailConfigurationEntity;
    }


    /// <summary>
    /// Atualiza a entidade de Client Email Configuration.
    /// </summary>
    /// <param name="updateClientEmailConfigurationRequest"></param>
    /// <param name="clientEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientTokenConfigurationEntity UpdateEntity(
        this UpdateClientTokenConfigurationRequest updateClientTokenConfigurationRequest, ClientTokenConfigurationEntity clientTokenConfigurationEntity)
    {
        clientTokenConfigurationEntity.Updated = DateTime.Now;
        clientTokenConfigurationEntity.Audience = updateClientTokenConfigurationRequest.Audience;
        clientTokenConfigurationEntity.Issuer = updateClientTokenConfigurationRequest.Issuer;
        clientTokenConfigurationEntity.SecurityKey = updateClientTokenConfigurationRequest.SecurityKey;
        clientTokenConfigurationEntity.Encrypted = updateClientTokenConfigurationRequest.Encrypted;
        clientTokenConfigurationEntity.AlgorithmJwsType = updateClientTokenConfigurationRequest.AlgorithmJwsType;
        clientTokenConfigurationEntity.AlgorithmJweType = updateClientTokenConfigurationRequest.AlgorithmJweType;

        return clientTokenConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de Send Grid Configuration
    /// </summary>
    /// <param name="updateSendGridConfigurationRequest"></param>
    /// <param name="sendGridConfigurationEntity"></param>
    /// <returns></returns>
    public static SendGridConfigurationEntity UpdateEntity(
        this UpdateSendGridConfigurationRequest updateSendGridConfigurationRequest, SendGridConfigurationEntity sendGridConfigurationEntity)
    {
        sendGridConfigurationEntity.Updated = DateTime.Now;
        sendGridConfigurationEntity.SendGridApiKey = updateSendGridConfigurationRequest.SendGridApiKey;
        sendGridConfigurationEntity.WelcomeTemplateId = updateSendGridConfigurationRequest.WelcomeTemplateId;

        return sendGridConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de Lockout Identity configuration.
    /// </summary>
    /// <param name="updateLockoutIdentityConfigurationRequest"></param>
    /// <param name="lockoutIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static LockoutIdentityConfigurationEntity UpdateEntity(
        this UpdateLockoutIdentityConfigurationRequest updateLockoutIdentityConfigurationRequest, LockoutIdentityConfigurationEntity lockoutIdentityConfigurationEntity)
    {
        lockoutIdentityConfigurationEntity.Updated = DateTime.Now;
        lockoutIdentityConfigurationEntity.AllowedForNewUsers = updateLockoutIdentityConfigurationRequest.AllowedForNewUsers;
        lockoutIdentityConfigurationEntity.DefaultLockoutTimeSpan = updateLockoutIdentityConfigurationRequest.DefaultLockoutTimeSpan;
        lockoutIdentityConfigurationEntity.MaxFailedAccessAttempts = updateLockoutIdentityConfigurationRequest.MaxFailedAccessAttempts;

        return lockoutIdentityConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de User identity configuration.
    /// </summary>
    /// <param name="updateUserIdentityConfigurationRequest"></param>
    /// <param name="userIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static UserIdentityConfigurationEntity UpdateEntity(
        this UpdateUserIdentityConfigurationRequest updateUserIdentityConfigurationRequest, UserIdentityConfigurationEntity userIdentityConfigurationEntity)
    {
        userIdentityConfigurationEntity.Updated = DateTime.Now;
        userIdentityConfigurationEntity.AllowedUserNameCharacters = updateUserIdentityConfigurationRequest.AllowedUserNameCharacters;
        userIdentityConfigurationEntity.RequireUniqueEmail = updateUserIdentityConfigurationRequest.RequireUniqueEmail;

        return userIdentityConfigurationEntity;
    }

    /// <summary>
    /// Transforma um Client Entity em response.
    /// </summary>
    /// <param name="clientEntity"></param>
    /// <returns></returns>
    public static ClientResponse ToResponse(this ClientEntity clientEntity,
        bool includeUsers = true, bool includeUserAdmins = true, bool includeClientConfig = true)
        => new()
        {
            Id = clientEntity.Id,
            Created = clientEntity.Created,
            Updated = clientEntity.Updated,
            Name = clientEntity.Name,
            Description = clientEntity.Description,
            Status = clientEntity.Status,

            UserAdmins = includeUserAdmins
                ? clientEntity?.UserAdmins?.Select(user => new ClientUserAdminResponse { ClientId = user.ClientId, UserId = user.UserId }).ToList()
                : [],

            Users = includeUsers
                ? clientEntity?.Users?.Select(user => user?.ToResponse()).ToList()
                : [],

            ClientConfiguration = includeClientConfig
                ? clientEntity?.ClientConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma Client config em Client config response.
    /// </summary>
    /// <param name="clientConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientConfigurationResponse ToResponse(this ClientConfigurationEntity clientConfigurationEntity,
        bool includeClient = false, bool includeClientIdentityConfiguration = true, bool includeClientEmailConfiguration = true, bool includeClientTokenConfiguration = true)
        => new()
        {
            Id = clientConfigurationEntity.Id,
            ClientId = clientConfigurationEntity.ClientId,
            Created = clientConfigurationEntity.Created,
            Updated = clientConfigurationEntity.Updated,
            ClientKey = clientConfigurationEntity.ClientKey,

            ClientIdentityConfiguration = includeClientIdentityConfiguration
                ? clientConfigurationEntity.ClientIdentityConfiguration?.ToResponse()
                : null,

            ClientEmailConfiguration = includeClientEmailConfiguration 
                ? clientConfigurationEntity.ClientEmailConfiguration?.ToResponse() 
                : null,

            ClientTokenConfiguration = includeClientTokenConfiguration
                ? clientConfigurationEntity.ClientTokenConfiguration?.ToResponse()
                : null,

            Client = includeClient
                ? clientConfigurationEntity.Client?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um ClientIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="clientIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientIdentityConfigurationResponse ToResponse(this ClientIdentityConfigurationEntity clientIdentityConfigurationEntity,
        bool includeClientConfiguration = false, bool includeUserIdentityConfiguration = true,
        bool includePasswordIdentityConfiguration = true, bool includeLockoutIdentityConfiguration = true)
        => new()
        {
            Id = clientIdentityConfigurationEntity.Id,
            ClientConfigurationId = clientIdentityConfigurationEntity.ClientConfigurationId,
            Created = clientIdentityConfigurationEntity.Created,
            Updated = clientIdentityConfigurationEntity.Updated,

            UserIdentityConfiguration = includeUserIdentityConfiguration
                ? clientIdentityConfigurationEntity?.UserIdentityConfiguration?.ToResponse()
                : null,

            PasswordIdentityConfiguration = includePasswordIdentityConfiguration
                ? clientIdentityConfigurationEntity?.PasswordIdentityConfiguration?.ToResponse()
                : null,

            LockoutIdentityConfiguration = includeLockoutIdentityConfiguration
                 ? clientIdentityConfigurationEntity.LockoutIdentityConfiguration?.ToResponse()
                : null,

            ClientConfiguration = includeClientConfiguration
                ? clientIdentityConfigurationEntity.ClientConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um UserIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="userIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static UserIdentityConfigurationResponse ToResponse(this UserIdentityConfigurationEntity userIdentityConfigurationEntity,
        bool includeClientIdentityConfiguration = false)
        => new()
        {
            Id = userIdentityConfigurationEntity.Id,
            Created = userIdentityConfigurationEntity.Created,
            Updated = userIdentityConfigurationEntity.Updated,
            AllowedUserNameCharacters = userIdentityConfigurationEntity.AllowedUserNameCharacters,
            RequireUniqueEmail = userIdentityConfigurationEntity.RequireUniqueEmail,
            ClientIdentityConfigurationId = userIdentityConfigurationEntity.ClientIdentityConfigurationId,
            ClientIdentityConfiguration = includeClientIdentityConfiguration
                ? userIdentityConfigurationEntity?.ClientIdentityConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um PasswordIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="passwordIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static PasswordIdentityConfigurationResponse ToResponse(this PasswordIdentityConfigurationEntity passwordIdentityConfigurationEntity,
        bool includeClientIdentityConfiguration = false)
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
            ClientIdentityConfigurationId = passwordIdentityConfigurationEntity.ClientIdentityConfigurationId,
            ClientIdentityConfiguration = includeClientIdentityConfiguration
                ? passwordIdentityConfigurationEntity?.ClientIdentityConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um LockoutIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="lockoutIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static LockoutIdentityConfigurationResponse ToResponse(this LockoutIdentityConfigurationEntity lockoutIdentityConfigurationEntity,
        bool includeClientIdentityConfiguration = false)
        => new()
        {
            Id = lockoutIdentityConfigurationEntity.Id,
            Created = lockoutIdentityConfigurationEntity.Created,
            Updated = lockoutIdentityConfigurationEntity.Updated,
            AllowedForNewUsers = lockoutIdentityConfigurationEntity.AllowedForNewUsers,
            DefaultLockoutTimeSpan = lockoutIdentityConfigurationEntity.DefaultLockoutTimeSpan,
            MaxFailedAccessAttempts = lockoutIdentityConfigurationEntity.MaxFailedAccessAttempts,
            ClientIdentityConfigurationId = lockoutIdentityConfigurationEntity.ClientIdentityConfigurationId,
            ClientIdentityConfiguration = includeClientIdentityConfiguration
                ? lockoutIdentityConfigurationEntity?.ClientIdentityConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um ClientEmailConfigurationEntity em response.
    /// </summary>
    /// <param name="clientEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientEmailConfigurationResponse ToResponse(this ClientEmailConfigurationEntity clientEmailConfigurationEntity,
        bool includeClientConfiguration = false)
        => new()
        {
            Id = clientEmailConfigurationEntity.Id,
            Created = clientEmailConfigurationEntity.Created,
            Updated = clientEmailConfigurationEntity.Updated,
            SendersName = clientEmailConfigurationEntity.SendersName,
            SendersEmail = clientEmailConfigurationEntity.SendersEmail,
            IsEmailConfirmed = clientEmailConfigurationEntity.IsEmailConfirmed,
            ClientConfigurationId = clientEmailConfigurationEntity.ClientConfigurationId,
            SendGridConfiguration = clientEmailConfigurationEntity?.SendGridConfiguration?.ToResponse(false),
            ClientConfiguration = includeClientConfiguration
                ? clientEmailConfigurationEntity?.ClientConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um SendGridConfigurationEntity em response.
    /// </summary>
    /// <param name="sendGridConfigurationEntity"></param>
    /// <param name="includeClientEmailConfiguration"></param>
    /// <returns></returns>
    public static SendGridConfigurationResponse ToResponse(this SendGridConfigurationEntity sendGridConfigurationEntity,
        bool includeClientEmailConfiguration)
        => new()
        {
            Id = sendGridConfigurationEntity.Id,
            Created = sendGridConfigurationEntity.Created,
            Updated = sendGridConfigurationEntity.Updated,
            SendGridApiKey = sendGridConfigurationEntity.SendGridApiKey,
            WelcomeTemplateId = sendGridConfigurationEntity.WelcomeTemplateId,
            ClientEmailConfigurationId = sendGridConfigurationEntity.ClientEmailConfigurationId,
            ClientEmailConfiguration = includeClientEmailConfiguration
                ? sendGridConfigurationEntity?.ClientEmailConfiguration?.ToResponse()
                : null
        };
  
    /// <summary>
    /// Transforma um ClientTokenConfigurationEntity em response.
    /// </summary>
    /// <param name="clientTokenConfigurationEntity"></param>
    /// <returns>ClientTokenConfigurationResponse</returns>
    public static ClientTokenConfigurationResponse ToResponse(this ClientTokenConfigurationEntity clientTokenConfigurationEntity,
        bool includeClientConfiguration = false)
        => new()
        {
            Id = clientTokenConfigurationEntity.Id,
            Created = clientTokenConfigurationEntity.Created,
            Updated = clientTokenConfigurationEntity.Updated,
            SecurityKey = clientTokenConfigurationEntity.SecurityKey,
            Issuer = clientTokenConfigurationEntity.Issuer,
            Audience = clientTokenConfigurationEntity.Audience,
            Encrypted = clientTokenConfigurationEntity.Encrypted,
            AlgorithmJwsType = clientTokenConfigurationEntity.AlgorithmJwsType,
            AlgorithmJweType = clientTokenConfigurationEntity.AlgorithmJweType,
            ClientConfigurationId = clientTokenConfigurationEntity.ClientConfigurationId,
            ClientConfiguration = includeClientConfiguration
                ? clientTokenConfigurationEntity?.ClientConfiguration?.ToResponse()
                : null
        };
}
