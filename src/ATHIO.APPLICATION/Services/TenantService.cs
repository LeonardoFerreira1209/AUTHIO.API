﻿using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.Email;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Dtos.ServiceBus.Events;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Exceptions;
using AUTHIO.DOMAIN.Helpers.Consts;
using AUTHIO.DOMAIN.Helpers.Expressions.Filters;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.DOMAIN.Validators;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.DOMAIN.Exceptions.CustomTenantExceptions;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Classe de serviço de Tenants.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class TenantService(
    IOptions<AppSettings> appSettings,
    IUnitOfWork<AuthIoContext> unitOfWork,
    ITenantRepository tenantRepository,
    ITenantConfigurationRepository tenantConfigurationRepository,
    ITenantIdentityConfigurationRepository tenantIdentityConfigurationRepository,
    IUserIdentityConfigurationRepository userIdentityConfigurationRepository,
    IPasswordIdentityConfigurationRepository passwordIdentityConfigurationRepository,
    ILockoutIdentityConfigurationRepository lockoutIdentityConfigurationRepository,
    ITenantEmailConfigurationRepository tenantEmailConfigurationRepository,
    ITenantTokenConfigurationRepository tenantTokenConfigurationRepository,
    ISendGridConfigurationRepository sendGridConfigurationRepository,
    IEventRepository eventRepository,
    IContextService contextService,
    IEmailProviderFactory emailProviderFactory,
    ICachingService cachingService,
    CustomUserManager<UserEntity> customUserManager) : ITenantService
{
    private readonly IEmailProvider emailProvider
       = emailProviderFactory.GetSendGridEmailProvider();

    private readonly string sendGridApiKey
        = Environment.GetEnvironmentVariable("SENDGRID_APIKEY")
                ?? appSettings.Value.Email.SendGrid.ApiKey;

    /// <summary>
    /// Id do usuário logado.
    /// </summary>
    private readonly Guid CurrentUserId
        = contextService.GetCurrentUserId();

    /// <summary>
    /// Método responsável por criar um Tenant.
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="DuplicatedTenantException"></exception>
    public async Task<ObjectResult> CreateAsync(
        CreateTenantRequest createTenantRequest, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(CreateAsync)}\n");

        try
        {
            await new CreateTenantRequestValidator()
                .ValidateAsync(createTenantRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await tenantRepository.GetAsync(tenant => tenant.Name == createTenantRequest.Name)
                .ContinueWith(async (tenantResult) =>
                {
                    if (tenantResult.Result is not null)
                        throw new DuplicatedTenantException(createTenantRequest);

                    var _transaction
                        = await unitOfWork.BeginTransactAsync();

                    try
                    {
                        return await tenantRepository.CreateAsync(
                           createTenantRequest.ToEntity(contextService.GetCurrentUserId()))
                               .ContinueWith(async (tenantEntityTask) =>
                               {
                                   var tenant
                                       = tenantEntityTask.Result;

                                   await tenantConfigurationRepository.CreateAsync(
                                        CreateTenantConfiguration.CreateDefault(
                                            tenant.Id)).ContinueWith(
                                               async (tenantConfigurationEntityTask) =>
                                               {
                                                   var tenantConfiguration
                                                        = tenantConfigurationEntityTask.Result;

                                                   await tenantIdentityConfigurationRepository.CreateAsync(
                                                       CreateTenantIdentityConfiguration.CreateDefault(
                                                            tenantConfiguration.Id)).ContinueWith(
                                                                async (tenantIdentityConfigurationEntityTask) =>
                                                                {
                                                                    var tenantIdentityConfiguration
                                                                        = tenantIdentityConfigurationEntityTask.Result;

                                                                    await userIdentityConfigurationRepository.CreateAsync(
                                                                         CreateUserIdentityConfiguration.CreateDefault(
                                                                             tenantIdentityConfiguration.Id)
                                                                         );

                                                                    await passwordIdentityConfigurationRepository.CreateAsync(
                                                                        CreatePasswordIdentityConfiguration.CreateDefault(
                                                                            tenantIdentityConfiguration.Id)
                                                                        );

                                                                    await lockoutIdentityConfigurationRepository.CreateAsync(
                                                                        CreateLockoutIdentityConfiguration.CreateDefault(
                                                                            tenantIdentityConfiguration.Id)
                                                                        );

                                                                    await tenantEmailConfigurationRepository.CreateAsync(
                                                                        CreateTenantEmailConfiguration.CreateDefault(
                                                                            tenantConfiguration.Id,
                                                                                createTenantRequest.Name, createTenantRequest.Email, true)

                                                                        ).ContinueWith(async (tenantEmailConfigurationTask) =>
                                                                        {
                                                                            var tenantEmailConfiguration
                                                                                = tenantEmailConfigurationTask.Result;

                                                                            await sendGridConfigurationRepository.CreateAsync(
                                                                                CreateSendGridConfiguration.CreateDefault(
                                                                                    tenantEmailConfiguration.Id,
                                                                                    createTenantRequest.SendGridApiKey ?? sendGridApiKey,
                                                                                    createTenantRequest.WelcomeTemplateId)
                                                                                );

                                                                        }).Unwrap();

                                                                    TokenConfigurationRequest tokenConfiguration 
                                                                        = createTenantRequest.TokenConfiguration;

                                                                    await tenantTokenConfigurationRepository.CreateAsync(
                                                                        CreateTenantTokenConfiguration.CreateDefault(
                                                                                tenantConfiguration.Id,
                                                                                tokenConfiguration.SecurityKey,
                                                                                tokenConfiguration.Issuer,
                                                                                tokenConfiguration.Audience
                                                                            )
                                                                        );

                                                                    await tenantEmailConfigurationRepository.CreateAsync(
                                                                        CreateTenantEmailConfiguration.CreateDefault(
                                                                            tenantConfiguration.Id,
                                                                                createTenantRequest.Name, createTenantRequest.Email, true)

                                                                        ).ContinueWith(async (tenantEmailConfigurationTask) =>
                                                                        {
                                                                            var tenantEmailConfiguration
                                                                                = tenantEmailConfigurationTask.Result;

                                                                            await sendGridConfigurationRepository.CreateAsync(
                                                                                CreateSendGridConfiguration.CreateDefault(
                                                                                    tenantEmailConfiguration.Id,
                                                                                    createTenantRequest.SendGridApiKey ?? sendGridApiKey,
                                                                                    createTenantRequest.WelcomeTemplateId)
                                                                                );

                                                                        }).Unwrap();

                                                                    await unitOfWork.CommitAsync();

                                                                }).Unwrap();
                                               }).Unwrap();

                                   await _transaction.CommitAsync();

                                   return new OkObjectResult(
                                       new ApiResponse<TenantResponse>(
                                           true,
                                           HttpStatusCode.Created,
                                           tenant.ToResponse(), [new DadosNotificacao("Tenant criado com sucesso!")])
                                       );

                               }).Unwrap();
                    }
                    catch (Exception exception)
                    {
                        Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

                        await _transaction.RollbackAsync(); throw;
                    }

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Recupera todos os dados de tenants.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> GetAllAsync(FilterRequest filterRequest, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(GetAllAsync)}\n");

        string cacheKey =
            $"getall-tenants-{filterRequest.PageNumber}-{filterRequest.PageSize}-{CurrentUserId}";

        ObjectResult tenantsCache =
            await cachingService
                .GetAsync<ObjectResult>(cacheKey);

        if (tenantsCache is not null)
            return tenantsCache;

        try
        {
            return await tenantRepository.GetAllAsyncPaginated(
                filterRequest.PageNumber,
                filterRequest.PageSize,
                TenantFilters.FilterByAdmin(
                    contextService.GetCurrentUserId()
                )
            )
            .ContinueWith(async (taskResult) =>
            {
                var pagination
                        = taskResult.Result;

                ObjectResult response = new(
                    new PaginationApiResponse<TenantResponse>(
                        true,
                        HttpStatusCode.OK,
                        pagination.ConvertPaginationData
                            (pagination.Items.Select(
                                tenant => tenant.ToResponse()).ToList()), [
                                    new DadosNotificacao("Tenants reuperados com sucesso!")]
                                )
                    );

                await cachingService
                    .SetAsync(cacheKey, response);

                return response;

            }).Unwrap() ;
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Recupera um tenant pela chave.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> GetTenantByKeyAsync(string key, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(GetTenantByKeyAsync)}\n");

        string cacheKey = $"get-tenant-{key}";

        ObjectResult tenantCache =
           await cachingService
               .GetAsync<ObjectResult>(cacheKey);

        if (tenantCache is not null)
            return tenantCache;

        try
        {
            return await tenantRepository.GetAsync(
                x => x.TenantConfiguration.TenantKey == key)
                    .ContinueWith(async (taskResult) =>
                    {
                        var tenantEntity
                                = taskResult.Result;

                        ObjectResult response = new(
                            new ApiResponse<TenantResponse>(
                                true,
                                HttpStatusCode.OK,
                                tenantEntity.ToResponse(), [
                                new DadosNotificacao("Tenant recuperado com sucesso!")]));

                        await cachingService
                            .SetAsync(cacheKey, response);

                        return response;

                    }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    ///  Método responsável por criar um usuário no tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundTenantException"></exception>
    /// <exception cref="NotPermissionTenantException"></exception>
    /// <exception cref="CreateUserFailedException"></exception>
    public async Task<ObjectResult> RegisterTenantUserAsync(
        RegisterUserRequest registerUserRequest, string tenantKey, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(RegisterTenantUserAsync)}\n");

        try
        {
            await new RegisterUserRequestValidator()
                .ValidateAsync(registerUserRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            var transaction =
                await unitOfWork.BeginTransactAsync();

            try
            {
                return await tenantRepository.GetAsync(tenant => tenant.TenantConfiguration.TenantKey.Equals(tenantKey))
                    .ContinueWith(async (taskResut) =>
                    {
                        var tenantEntity
                          = taskResut.Result
                             ?? throw new NotFoundTenantException(tenantKey);

                        if (!tenantEntity.UserId.Equals(contextService.GetCurrentUserId()))
                            throw new NotPermissionTenantException();

                        var userEntity
                           = registerUserRequest.ToUserTenantEntity(tenantEntity.Id);

                        return await customUserManager.CreateAsync(
                             userEntity, registerUserRequest.Password)
                                .ContinueWith(async identityResultTask =>
                                {
                                    var identityResult
                                            = identityResultTask.Result;

                                    if (identityResult.Succeeded is false)
                                        throw new CreateUserFailedException(
                                            registerUserRequest, identityResult.Errors.Select((e)
                                                => new DadosNotificacao(e.Description)).ToList());

                                    var jsonBody = JsonConvert.SerializeObject(new EmailEvent(CreateDefaultEmailMessage
                                               .CreateWithHtmlContent(userEntity.FirstName, userEntity.Email,
                                                  EmailConst.SUBJECT_CONFIRMACAO_EMAIL, EmailConst.PLAINTEXTCONTENT_CONFIRMACAO_EMAIL, EmailConst.HTML_CONTENT_CONFIRMACAO_EMAIL)));

                                    await eventRepository.CreateAsync(CreateEvent
                                       .CreateEmailEvent(jsonBody)).ContinueWith(async (task) => {
                                           await unitOfWork.CommitAsync();
                                           await transaction.CommitAsync();
                                       }).Unwrap();

                                    return new ObjectResult(
                                        new ApiResponse<UserResponse>(
                                            identityResult.Succeeded,
                                                HttpStatusCode.Created,
                                                userEntity.ToResponse(), [
                                                     new DadosNotificacao("Usuário criado com sucesso e vinculado ao Tenant!")]));
                                }).Unwrap();

                    }).Unwrap();
            }
            catch   
            {
                transaction.Rollback(); throw;
            }
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }
}
