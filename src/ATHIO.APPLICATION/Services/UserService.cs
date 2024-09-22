using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Dtos.ServiceBus.Events;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Consts;
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
    IUnitOfWork<AuthIoContext> unitOfWork,
    IEventRepository eventRepository) : IUserService
{
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
                var userEntity
                    = registerUserRequest.ToUserSystemEntity();

                return await customUserManager
                    .CreateAsync(userEntity, registerUserRequest.Password).ContinueWith(async (identityResultTask) =>
                    {
                        var identityResult
                                = identityResultTask.Result;

                        if (identityResult.Succeeded is false)
                            throw new CreateUserFailedException(
                                registerUserRequest, identityResult.Errors.Select((e)
                                    => new DataNotifications(e.Description)).ToList());

                        return await customUserManager.AddToRoleAsync(
                            userEntity, "System").ContinueWith(async (identityResultTask) =>
                            {
                                var identityResult
                                        = identityResultTask.Result;

                                if (identityResult.Succeeded is false)
                                    throw new UserToRoleFailedException(
                                        registerUserRequest, identityResult.Errors.Select((e)
                                            => new DataNotifications(e.Description)).ToList());

                                var jsonBody = JsonConvert.SerializeObject(new EmailEvent(CreateDefaultEmailMessage
                                            .CreateWithHtmlContent(userEntity.FirstName, userEntity.Email,
                                               EmailConst.SUBJECT_CONFIRMACAO_EMAIL, EmailConst.PLAINTEXTCONTENT_CONFIRMACAO_EMAIL, EmailConst.HTML_CONTENT_CONFIRMACAO_EMAIL)));

                                await eventRepository.CreateAsync(CreateEvent
                                    .CreateEmailEvent(jsonBody)).ContinueWith(async (task) =>
                                    {
                                        await unitOfWork.CommitAsync();
                                        await transaction.CommitAsync();
                                    }).Unwrap();

                                return new ObjectResponse(
                                    HttpStatusCode.Created,
                                    new ApiResponse<UserResponse>(
                                        identityResult.Succeeded,
                                        HttpStatusCode.Created,
                                        userEntity.ToResponse(), [
                                            new DataNotifications("Usuário criado com sucesso!")]));
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
