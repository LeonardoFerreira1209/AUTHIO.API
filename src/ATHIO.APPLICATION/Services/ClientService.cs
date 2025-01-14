﻿using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.Email;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
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
using static AUTHIO.DOMAIN.Exceptions.CustomClientExceptions;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Classe de serviço de Clients.
/// </summary>
/// <param name="appSettings"></param>
/// <param name="unitOfWork"></param>
/// <param name="clientRepository"></param>
/// <param name="eventRepository"></param>
/// <param name="contextService"></param>
/// <param name="emailProviderFactory"></param>
/// <param name="cachingService"></param>
/// <param name="customUserManager"></param>
public class Clientservice(
    IOptions<AppSettings> appSettings,
    IUnitOfWork<AuthIoContext> unitOfWork,
    IClientRepository clientRepository,
    IEventRepository eventRepository,
    IContextService contextService,
    IEmailProviderFactory emailProviderFactory,
    ICachingService cachingService,
    CustomUserManager<UserEntity> customUserManager) : IClientservice
{
    /// <summary>
    /// Email provider.
    /// </summary>
    private readonly IEmailProvider emailProvider
       = emailProviderFactory.GetSendGridEmailProvider();

    /// <summary>
    /// SendGrid apikey.
    /// </summary>
    private readonly string sendGridApiKey
        = Environment.GetEnvironmentVariable("SENDGRID_APIKEY")
                ?? appSettings.Value.Email.SendGrid.ApiKey;

    /// <summary>
    /// Id do usuário logado.
    /// </summary>
    private readonly Guid CurrentUserId
        = contextService.GetCurrentUserId();

    /// <summary>
    /// Método responsável por criar um Client.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="DuplicatedClientException"></exception>
    public async Task<ObjectResult> CreateAsync(
        CreateClientRequest createClientRequest,
        Guid realmId,
        CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(Clientservice)} - METHOD {nameof(CreateAsync)}\n");

        try
        {
            await new CreateClientRequestValidator()
                .ValidateAsync(createClientRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await clientRepository.GetAsync(
                client => client.Name.Equals(
                    createClientRequest.Name
                )
            )
            .ContinueWith(async (clientResult) =>
            {
                if (clientResult.Result is not null)
                    throw new DuplicatedClientException(createClientRequest);

                ClientEntity client
                    = await clientRepository
                        .CreateAsync(
                            createClientRequest
                                .ToEntity(
                                    CurrentUserId, 
                                    realmId
                                )
                        );

                await unitOfWork.CommitAsync();

                return new ObjectResponse(
                    HttpStatusCode.Created,
                    new ApiResponse<ClientResponse>(
                        true,
                        HttpStatusCode.Created,
                        client.ToResponse(), [new DataNotifications("Client criado com sucesso!")]
                    )
                );

            }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    /// Método responsável por atualizar um Client.
    /// </summary>
    /// <param name="updateClientRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundClientException"></exception>
    public async Task<ObjectResult> UpdateAsync(
       UpdateClientRequest updateClientRequest,
       CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(Clientservice)} - METHOD {nameof(UpdateAsync)}\n");

        try
        {
            await new UpdateClientRequestValidator()
                .ValidateAsync(updateClientRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await clientRepository.GetByIdAsync(
                updateClientRequest.Id

            ).ContinueWith(async (clientEntityTask) =>
            {
                var client
                    = clientEntityTask.Result
                    ?? throw new NotFoundClientException();

                await clientRepository.UpdateAsync(
                    updateClientRequest
                        .UpdateEntity(client)
                );

                await unitOfWork.CommitAsync();

                return new ObjectResponse(
                    HttpStatusCode.OK,
                    new ApiResponse<ClientResponse>(
                        true,
                        HttpStatusCode.OK,
                        client.ToResponse(), [new DataNotifications("Client atualizado com sucesso!")]
                    )
                );

            }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    /// Recupera todos os dados de Clients.
    /// </summary>
    /// <param name="filterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> GetAllAsync(
        FilterRequest filterRequest,
        CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(Clientservice)} - METHOD {nameof(GetAllAsync)}\n");

        string cacheKey =
            $"getall-Clients-{filterRequest.PageNumber}-{filterRequest.PageSize}-{CurrentUserId}";

        ObjectResult clientCache =
            await cachingService
                .GetAsync<ObjectResult>(cacheKey);

        if (clientCache is not null)
            return clientCache;

        try
        {
            return await clientRepository.GetAllAsyncPaginated(
                filterRequest.PageNumber,
                filterRequest.PageSize,
                ClientFilters.FilterByAdmin(
                    contextService.GetCurrentUserId()
                )
            )
            .ContinueWith(async (taskResult) =>
            {
                var pagination
                        = taskResult.Result;

                ObjectResponse response = new(
                    HttpStatusCode.OK,
                    new PaginationApiResponse<ClientResponse>(
                        true,
                        HttpStatusCode.OK,
                        pagination.ConvertPaginationData
                            (pagination.Items.Select(
                                Client => Client.ToResponse()).ToList()), [
                                    new DataNotifications("Clients reuperados com sucesso!")]
                                )
                    );

                await cachingService
                    .SetAsync(cacheKey, response);

                return response;

            }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    /// Recupera um Client pela chave.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> GetClientByKeyAsync(
        string key,
        CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(Clientservice)} - METHOD {nameof(GetClientByKeyAsync)}\n");

        string cacheKey = $"get-Client-{key}";

        ObjectResult clientCache =
           await cachingService
               .GetAsync<ObjectResult>(cacheKey);

        if (clientCache is not null)
            return clientCache;

        try
        {
            return await clientRepository.GetAsync(
                x => x.ClientConfiguration.ClientKey == key
            )
            .ContinueWith(async (taskResult) =>
            {
                var clientEntity
                        = taskResult.Result;

                ObjectResponse response = new(
                    HttpStatusCode.OK,
                    new ApiResponse<ClientResponse>(
                        true,
                        HttpStatusCode.OK,
                        clientEntity.ToResponse(), [
                        new DataNotifications("Client recuperado com sucesso!")]
                    )
                );

                await cachingService
                    .SetAsync(cacheKey, response);

                return response;

            }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    ///  Método responsável por criar um usuário no Client.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="clientKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundClientException"></exception>
    /// <exception cref="NotPermissionClientException"></exception>
    /// <exception cref="CreateUserFailedException"></exception>
    public async Task<ObjectResult> RegisterClientUserAsync(
        RegisterUserRequest registerUserRequest,
        string clientKey,
        CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(Clientservice)} - METHOD {nameof(RegisterClientUserAsync)}\n");

        var transaction =
            await unitOfWork.BeginTransactAsync();

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

            return await clientRepository.GetAsync(
                client => client.ClientConfiguration.ClientKey.Equals(
                    clientKey
                )
            )
            .ContinueWith(async (taskResut) =>
            {
                var clientEntity
                    = taskResut.Result
                        ?? throw new NotFoundClientException(clientKey);

                if (!clientEntity.UserId.Equals(contextService.GetCurrentUserId()))
                    throw new NotPermissionClientException();

                var userEntity
                    = registerUserRequest.ToUserClientEntity(clientEntity.Id);

                return await customUserManager.CreateAsync(
                    userEntity, registerUserRequest.Password)
                    .ContinueWith(async identityResultTask =>
                    {
                        var identityResult
                                = identityResultTask.Result;

                        if (identityResult.Succeeded is false)
                            throw new CreateUserFailedException(
                                registerUserRequest, identityResult.Errors.Select((e)
                                    => new DataNotifications(e.Description)).ToList()
                            );

                        var jsonBody = JsonConvert.SerializeObject(new EmailEvent(CreateDefaultEmailMessage
                                .CreateWithHtmlContent(userEntity.FirstName, userEntity.Email,
                                    EmailConst.SUBJECT_CONFIRMACAO_EMAIL, EmailConst.PLAINTEXTCONTENT_CONFIRMACAO_EMAIL, EmailConst.HTML_CONTENT_CONFIRMACAO_EMAIL)
                                )
                        );

                        await eventRepository.CreateAsync(CreateEvent
                            .CreateEmailEvent(jsonBody)
                        );

                        await unitOfWork.CommitAsync();
                        await transaction.CommitAsync();

                        return new ObjectResponse(
                            HttpStatusCode.Created,
                            new ApiResponse<UserResponse>(
                                identityResult.Succeeded,
                                    HttpStatusCode.Created,
                                    userEntity.ToResponse(), [
                                            new DataNotifications(
                                                "Usuário criado com sucesso e vinculado ao Client!"
                                            )
                                    ]
                            )
                        );

                    }).Unwrap();

            }).Unwrap();
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(
                cancellationToken
            );

            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}