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
    public static ClientEntity ToEntity(this CreateClientRequest createClientRequest, Guid userId)
        => CreateClient.CreateDefault(
            userId,
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
    /// <param name="ClientEntity"></param>
    /// <returns></returns>
    public static ClientEntity UpdateEntity(
        this UpdateClientRequest updateClientRequest, ClientEntity ClientEntity)
    {
        ClientEntity.Name = updateClientRequest.Name;
        ClientEntity.Description = updateClientRequest.Description;
        ClientEntity.Updated = DateTime.Now;

        ClientEntity.ClientConfiguration 
            = updateClientRequest
                ?.ClientConfiguration
                    ?.UpdateEntity(ClientEntity.ClientConfiguration) ?? ClientEntity.ClientConfiguration;

        return ClientEntity;
    }
    
    /// <summary>
    /// Atualiza a entidade de Client configuration.
    /// </summary>
    /// <param name="updateClientConfigurationRequest"></param>
    /// <param name="ClientConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientConfigurationEntity UpdateEntity(
        this UpdateClientConfigurationRequest updateClientConfigurationRequest, ClientConfigurationEntity ClientConfigurationEntity)
    {
        ClientConfigurationEntity.Updated = DateTime.Now;

        ClientConfigurationEntity.ClientIdentityConfiguration 
            = updateClientConfigurationRequest
                ?.ClientIdentityConfiguration
                    ?.UpdateEntity(ClientConfigurationEntity.ClientIdentityConfiguration) 
                        ?? ClientConfigurationEntity.ClientIdentityConfiguration;

        ClientConfigurationEntity.ClientEmailConfiguration 
            = updateClientConfigurationRequest
                ?.ClientEmailConfiguration
                    ?.UpdateEntity(ClientConfigurationEntity.ClientEmailConfiguration) 
                        ?? ClientConfigurationEntity.ClientEmailConfiguration;

        ClientConfigurationEntity.ClientTokenConfiguration
            = updateClientConfigurationRequest
                ?.ClientTokenConfiguration
                    ?.UpdateEntity(ClientConfigurationEntity.ClientTokenConfiguration) 
                        ?? ClientConfigurationEntity.ClientTokenConfiguration;

        return ClientConfigurationEntity;
    }

    /// <summary>
    /// Atualiza a entidade de Client identity configuration.
    /// </summary>
    /// <param name="updateClientIdentityConfigurationRequest"></param>
    /// <param name="ClientIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientIdentityConfigurationEntity UpdateEntity(
        this UpdateClientIdentityConfigurationRequest updateClientIdentityConfigurationRequest, ClientIdentityConfigurationEntity ClientIdentityConfigurationEntity)
    { 
        ClientIdentityConfigurationEntity.Updated = DateTime.Now;

        ClientIdentityConfigurationEntity.UserIdentityConfiguration 
            = updateClientIdentityConfigurationRequest
                ?.UserIdentityConfiguration
                    ?.UpdateEntity(ClientIdentityConfigurationEntity.UserIdentityConfiguration) 
                        ?? ClientIdentityConfigurationEntity.UserIdentityConfiguration;

        ClientIdentityConfigurationEntity.PasswordIdentityConfiguration 
            = updateClientIdentityConfigurationRequest
                ?.PasswordIdentityConfiguration
                    ?.UpdateEntity(ClientIdentityConfigurationEntity.PasswordIdentityConfiguration) 
                        ?? ClientIdentityConfigurationEntity.PasswordIdentityConfiguration;

        ClientIdentityConfigurationEntity.LockoutIdentityConfiguration 
            = updateClientIdentityConfigurationRequest
                ?.LockoutIdentityConfiguration
                    ?.UpdateEntity(ClientIdentityConfigurationEntity.LockoutIdentityConfiguration) 
                        ?? ClientIdentityConfigurationEntity.LockoutIdentityConfiguration;

        return ClientIdentityConfigurationEntity;
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
    /// <param name="ClientEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientEmailConfigurationEntity UpdateEntity(
        this UpdateClientEmailConfigurationRequest updateClientEmailConfigurationRequest, ClientEmailConfigurationEntity ClientEmailConfigurationEntity)
    {
        ClientEmailConfigurationEntity.Updated = DateTime.Now;
        ClientEmailConfigurationEntity.SendersName = updateClientEmailConfigurationRequest.SendersName;
        ClientEmailConfigurationEntity.SendersEmail = updateClientEmailConfigurationRequest.SendersEmail;
        ClientEmailConfigurationEntity.IsEmailConfirmed = updateClientEmailConfigurationRequest.IsEmailConfirmed;

        ClientEmailConfigurationEntity.SendGridConfiguration 
            = updateClientEmailConfigurationRequest
                ?.SendGridConfiguration
                    ?.UpdateEntity(ClientEmailConfigurationEntity.SendGridConfiguration) 
                        ?? ClientEmailConfigurationEntity.SendGridConfiguration;

        return ClientEmailConfigurationEntity;
    }


    /// <summary>
    /// Atualiza a entidade de Client Email Configuration.
    /// </summary>
    /// <param name="updateClientEmailConfigurationRequest"></param>
    /// <param name="ClientEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientTokenConfigurationEntity UpdateEntity(
        this UpdateClientTokenConfigurationRequest updateClientTokenConfigurationRequest, ClientTokenConfigurationEntity ClientTokenConfigurationEntity)
    {
        ClientTokenConfigurationEntity.Updated = DateTime.Now;
        ClientTokenConfigurationEntity.Audience = updateClientTokenConfigurationRequest.Audience;
        ClientTokenConfigurationEntity.Issuer = updateClientTokenConfigurationRequest.Issuer;
        ClientTokenConfigurationEntity.SecurityKey = updateClientTokenConfigurationRequest.SecurityKey;
        ClientTokenConfigurationEntity.Encrypted = updateClientTokenConfigurationRequest.Encrypted;
        ClientTokenConfigurationEntity.AlgorithmJwsType = updateClientTokenConfigurationRequest.AlgorithmJwsType;
        ClientTokenConfigurationEntity.AlgorithmJweType = updateClientTokenConfigurationRequest.AlgorithmJweType;

        return ClientTokenConfigurationEntity;
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
    /// <param name="ClientEntity"></param>
    /// <returns></returns>
    public static ClientResponse ToResponse(this ClientEntity ClientEntity,
        bool includeUsers = true, bool includeUserAdmins = true, bool includeClientConfig = true)
        => new()
        {
            Id = ClientEntity.Id,
            Created = ClientEntity.Created,
            Updated = ClientEntity.Updated,
            Name = ClientEntity.Name,
            Description = ClientEntity.Description,
            Status = ClientEntity.Status,

            UserAdmins = includeUserAdmins
                ? ClientEntity?.UserAdmins?.Select(user => new ClientUserAdminResponse { ClientId = user.ClientId, UserId = user.UserId }).ToList()
                : [],

            Users = includeUsers
                ? ClientEntity?.Users?.Select(user => user?.ToResponse()).ToList()
                : [],

            ClientConfiguration = includeClientConfig
                ? ClientEntity?.ClientConfiguration?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma Client config em Client config response.
    /// </summary>
    /// <param name="ClientConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientConfigurationResponse ToResponse(this ClientConfigurationEntity ClientConfigurationEntity,
        bool includeClient = false, bool includeClientIdentityConfiguration = true, bool includeClientEmailConfiguration = true, bool includeClientTokenConfiguration = true)
        => new()
        {
            Id = ClientConfigurationEntity.Id,
            ClientId = ClientConfigurationEntity.ClientId,
            Created = ClientConfigurationEntity.Created,
            Updated = ClientConfigurationEntity.Updated,
            ClientKey = ClientConfigurationEntity.ClientKey,

            ClientIdentityConfiguration = includeClientIdentityConfiguration
                ? ClientConfigurationEntity.ClientIdentityConfiguration?.ToResponse()
                : null,

            ClientEmailConfiguration = includeClientEmailConfiguration 
                ? ClientConfigurationEntity.ClientEmailConfiguration?.ToResponse() 
                : null,

            ClientTokenConfiguration = includeClientTokenConfiguration
                ? ClientConfigurationEntity.ClientTokenConfiguration?.ToResponse()
                : null,

            Client = includeClient
                ? ClientConfigurationEntity.Client?.ToResponse()
                : null
        };

    /// <summary>
    /// Transforma um ClientIdentityConfigurationEntity em response.
    /// </summary>
    /// <param name="ClientIdentityConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientIdentityConfigurationResponse ToResponse(this ClientIdentityConfigurationEntity ClientIdentityConfigurationEntity,
        bool includeClientConfiguration = false, bool includeUserIdentityConfiguration = true,
        bool includePasswordIdentityConfiguration = true, bool includeLockoutIdentityConfiguration = true)
        => new()
        {
            Id = ClientIdentityConfigurationEntity.Id,
            ClientConfigurationId = ClientIdentityConfigurationEntity.ClientConfigurationId,
            Created = ClientIdentityConfigurationEntity.Created,
            Updated = ClientIdentityConfigurationEntity.Updated,

            UserIdentityConfiguration = includeUserIdentityConfiguration
                ? ClientIdentityConfigurationEntity?.UserIdentityConfiguration?.ToResponse()
                : null,

            PasswordIdentityConfiguration = includePasswordIdentityConfiguration
                ? ClientIdentityConfigurationEntity?.PasswordIdentityConfiguration?.ToResponse()
                : null,

            LockoutIdentityConfiguration = includeLockoutIdentityConfiguration
                 ? ClientIdentityConfigurationEntity.LockoutIdentityConfiguration?.ToResponse()
                : null,

            ClientConfiguration = includeClientConfiguration
                ? ClientIdentityConfigurationEntity.ClientConfiguration?.ToResponse()
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
    /// <param name="ClientEmailConfigurationEntity"></param>
    /// <returns></returns>
    public static ClientEmailConfigurationResponse ToResponse(this ClientEmailConfigurationEntity ClientEmailConfigurationEntity,
        bool includeClientConfiguration = false)
        => new()
        {
            Id = ClientEmailConfigurationEntity.Id,
            Created = ClientEmailConfigurationEntity.Created,
            Updated = ClientEmailConfigurationEntity.Updated,
            SendersName = ClientEmailConfigurationEntity.SendersName,
            SendersEmail = ClientEmailConfigurationEntity.SendersEmail,
            IsEmailConfirmed = ClientEmailConfigurationEntity.IsEmailConfirmed,
            ClientConfigurationId = ClientEmailConfigurationEntity.ClientConfigurationId,
            SendGridConfiguration = ClientEmailConfigurationEntity?.SendGridConfiguration?.ToResponse(false),
            ClientConfiguration = includeClientConfiguration
                ? ClientEmailConfigurationEntity?.ClientConfiguration?.ToResponse()
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
    /// <param name="ClientTokenConfigurationEntity"></param>
    /// <returns>ClientTokenConfigurationResponse</returns>
    public static ClientTokenConfigurationResponse ToResponse(this ClientTokenConfigurationEntity ClientTokenConfigurationEntity,
        bool includeClientConfiguration = false)
        => new()
        {
            Id = ClientTokenConfigurationEntity.Id,
            Created = ClientTokenConfigurationEntity.Created,
            Updated = ClientTokenConfigurationEntity.Updated,
            SecurityKey = ClientTokenConfigurationEntity.SecurityKey,
            Issuer = ClientTokenConfigurationEntity.Issuer,
            Audience = ClientTokenConfigurationEntity.Audience,
            Encrypted = ClientTokenConfigurationEntity.Encrypted,
            AlgorithmJwsType = ClientTokenConfigurationEntity.AlgorithmJwsType,
            AlgorithmJweType = ClientTokenConfigurationEntity.AlgorithmJweType,
            ClientConfigurationId = ClientTokenConfigurationEntity.ClientConfigurationId,
            ClientConfiguration = includeClientConfiguration
                ? ClientTokenConfigurationEntity?.ClientConfiguration?.ToResponse()
                : null
        };
}
