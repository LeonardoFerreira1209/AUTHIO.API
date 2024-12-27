using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Dtos.ServiceBus.Events;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Consts;
using AUTHIO.DOMAIN.Helpers.Expressions;
using AUTHIO.DOMAIN.Helpers.Expressions.Filters;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.DOMAIN.Validators;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Serviço de usuarios.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class UserService(
    CustomUserManager<UserEntity> customUserManager,
    IContextService contextService,
    IUnitOfWork<AuthIoContext> unitOfWork,
    IEventRepository eventRepository) : IUserService
{
    /// <summary>
    /// Recupera o id do usuário atual.
    /// </summary>
    private readonly Guid CurrentUserId
        = contextService.GetCurrentUserId();

    /// <summary>
    /// Busca um usuário por Id.
    /// </summary>
    /// <param name="idWithXTenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> GetUserByIdAsync(
        IdWithXTenantKey idWithXTenantKey,
        CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(UserService)} - METHOD {nameof(GetUserByIdAsync)}\n");

        try
        {
            var user = await customUserManager
                .GetUserByIdAsync(
                    idWithXTenantKey.Id,
                    CustomLambdaExpressions.Or(
                        x => x.Id == CurrentUserId,
                        UserFilters<UserEntity>.FilterTenantUsers(
                            idWithXTenantKey.TenantKey
                        )
                    ),
                    cancellationToken
                );

            return new ObjectResponse(
                HttpStatusCode.OK,
                new ApiResponse<UserResponse>(
                    true,
                    HttpStatusCode.OK,
                    user?.ToResponse(), [
                        new DataNotifications("Usuário recuperado com sucesso!")
                    ]
                )
            );
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    /// Método de registro de usuário.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="CreateUserFailedException"></exception>
    public async Task<ObjectResult> RegisterAsync(
        RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(UserService)} - METHOD {nameof(RegisterAsync)}\n");

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

            var userEntity
                = registerUserRequest.ToUserSystemEntity();

            return await customUserManager
                .CreateAsync(
                    userEntity,
                    registerUserRequest.Password

                ).ContinueWith(async (identityResultTask) =>
                {
                    var identityResult
                            = identityResultTask.Result;

                    if (identityResult.Succeeded is false)
                        throw new CreateUserFailedException(
                            registerUserRequest, identityResult.Errors.Select((e)
                                => new DataNotifications(e.Description)).ToList()
                        );

                    return await customUserManager.AddToRoleAsync(
                        userEntity, "System").ContinueWith(async (identityResultTask) =>
                        {
                            var identityResult
                                    = identityResultTask.Result;

                            if (identityResult.Succeeded is false)
                                throw new UserToRoleFailedException(
                                    registerUserRequest, identityResult.Errors.Select((e)
                                        => new DataNotifications(e.Description)).ToList()
                                );

                            var jsonBody = JsonConvert.SerializeObject(new EmailEvent(CreateDefaultEmailMessage
                                    .CreateWithHtmlContent(userEntity.FirstName, userEntity.Email,
                                        EmailConst.SUBJECT_CONFIRMACAO_EMAIL, EmailConst.PLAINTEXTCONTENT_CONFIRMACAO_EMAIL, EmailConst.HTML_CONTENT_CONFIRMACAO_EMAIL)
                                    )
                            );

                            await eventRepository.CreateAsync(
                                CreateEvent.CreateEmailEvent(
                                    jsonBody
                                )
                            );

                            await unitOfWork.CommitAsync();
                            await transaction.CommitAsync();

                            return new ObjectResponse(
                                HttpStatusCode.Created,
                                new ApiResponse<UserResponse>(
                                    identityResult.Succeeded,
                                    HttpStatusCode.Created,
                                    userEntity.ToResponse(), [
                                        new DataNotifications("Usuário criado com sucesso!")
                                    ]
                                )
                            );

                        }).Unwrap();

                }).Unwrap();
        }
        catch (Exception exception)
        {
            transaction.Rollback();

            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
