using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Exceptions;
using AUTHIO.DOMAIN.Helpers.Expressions.Filters;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.DOMAIN.Validators;
using AUTHIO.INFRASTRUCTURE.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.DOMAIN.Exceptions.CustomRealmExceptions;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Classe de serviço de Realms.
/// </summary>
/// <param name="unitOfWork"></param>
/// <param name="realmRepository"></param>
/// <param name="contextService"></param>
/// <param name="cachingService"></param>
public class RealmService(
    IUnitOfWork<AuthIoContext> unitOfWork,
    IRealmRepository realmRepository,
    IClientRepository clientRepository,
    IContextService contextService,
    ICachingService cachingService) : IRealmService
{

    /// <summary>
    /// Id do usuário logado.
    /// </summary>
    private readonly Guid CurrentUserId
        = contextService.GetCurrentUserId();

    /// <summary>
    /// Método responsável por criar um Realm.
    /// </summary>
    /// <param name="createRealmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="DuplicatedRealmException"></exception>
    public async Task<ObjectResult> CreateAsync(
        CreateRealmRequest createRealmRequest,
        CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(RealmService)} - METHOD {nameof(CreateAsync)}\n");

        try
        {
            await new CreateRealmRequestValidator()
                .ValidateAsync(createRealmRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await realmRepository.GetAsync(
                client => client.Name.Equals(
                    createRealmRequest.Name
                )
            )
            .ContinueWith(async (clientResult) =>
            {
                if (clientResult.Result is not null)
                    throw new DuplicatedRealmException(createRealmRequest);

                RealmEntity realm
                    = await realmRepository
                        .CreateAsync(
                            createRealmRequest.ToEntity(CurrentUserId)
                        );

                await unitOfWork.CommitAsync();

                return new ObjectResponse(
                    HttpStatusCode.Created,
                    new ApiResponse<RealmResponse>(
                        true,
                        HttpStatusCode.Created,
                        realm.ToResponse(), [new DataNotifications("Realm criado com sucesso!")]
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
    /// Método responsável por atualizar um Realm.
    /// </summary>
    /// <param name="updateRealmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundRealmException"></exception>
    public async Task<ObjectResult> UpdateAsync(
       UpdateRealmRequest updateRealmRequest,
       CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(RealmService)} - METHOD {nameof(UpdateAsync)}\n");

        try
        {
            await new UpdateRealmRequestValidator()
                .ValidateAsync(updateRealmRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await realmRepository.GetByIdAsync(
                updateRealmRequest.Id

            ).ContinueWith(async (realmEntityTask) =>
            {
                var realm
                    = realmEntityTask.Result
                    ?? throw new NotFoundRealmException();

                await realmRepository.UpdateAsync(
                    updateRealmRequest
                        .UpdateEntity(realm)
                );

                await unitOfWork.CommitAsync();

                return new ObjectResponse(
                    HttpStatusCode.OK,
                    new ApiResponse<RealmResponse>(
                        true,
                        HttpStatusCode.OK,
                        realm.ToResponse(), [new DataNotifications("Realm atualizado com sucesso!")]
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
    /// Recupera todos os dados de Realms.
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
            $"[LOG INFORMATION] - SET TITLE {nameof(RealmService)} - METHOD {nameof(GetAllAsync)}\n");

        string cacheKey =
            $"getall-Realms-{filterRequest.PageNumber}-{filterRequest.PageSize}-{CurrentUserId}";

        ObjectResult realmCache =
            await cachingService
                .GetAsync<ObjectResult>(cacheKey);

        if (realmCache is not null)
            return realmCache;

        try
        {
            return await realmRepository.GetAllAsyncPaginated(
                filterRequest.PageNumber,
                filterRequest.PageSize,
                RealmFilters.FilterByAdmin(
                    contextService.GetCurrentUserId()
                )
            )
            .ContinueWith(async (taskResult) =>
            {
                var pagination
                        = taskResult.Result;

                ObjectResponse response = new(
                    HttpStatusCode.OK,
                    new PaginationApiResponse<RealmResponse>(
                        true,
                        HttpStatusCode.OK,
                        pagination.ConvertPaginationData
                            (pagination.Items.Select(
                                realm => realm.ToResponse()).ToList()), [
                                    new DataNotifications("Realms reuperados com sucesso!")]
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
    /// Método responsável por criar um client no Realm.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <param name="realmId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundRealmException"></exception>
    /// <exception cref="NotPermissionRealmException"></exception>
    /// <exception cref="CreateClientFailedException"></exception>
    public async Task<ObjectResult> RegisterRealmClientAsync(
        CreateClientRequest createClientRequest,
        Guid realmId,
        CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(RealmService)} - METHOD {nameof(RegisterRealmClientAsync)}\n");

        var transaction =
            await unitOfWork.BeginTransactAsync();

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

            return await realmRepository.GetAsync(
                realm => realm.Id.Equals(
                    realmId
                )
            )
            .ContinueWith(async (taskResut) =>
            {
                var realmEntity
                    = taskResut.Result
                        ?? throw new NotFoundRealmException(realmId);

                if (!realmEntity.UserId.Equals(
                    contextService.GetCurrentUserId()

                )) throw new NotPermissionRealmException();

                var clientEntity
                    = createClientRequest.ToEntity(
                        realmEntity.UserId,
                        realmId
                    );

                return await clientRepository.CreateAsync(
                    clientEntity
                )
                .ContinueWith(async taskResult =>
                {
                    await unitOfWork.CommitAsync();
                    await transaction.CommitAsync();

                    return new ObjectResponse(
                        HttpStatusCode.Created,
                        new ApiResponse<ClientResponse>(
                                true,
                                HttpStatusCode.Created,
                                clientEntity.ToResponse(), [
                                    new DataNotifications(
                                        "Client criado com sucesso e vinculado ao Realm!"
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
