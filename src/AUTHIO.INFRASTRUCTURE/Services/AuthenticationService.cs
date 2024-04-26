﻿

using AUTHIO.DATABASE.Context;
using AUTHIO.DOMAIN.Auth.Token;
using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.DOMAIN.Validators;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de Authenticação do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class AuthenticationService(
    ITokenService tokenService,
    IUserRepository userRepository,
    IUnitOfWork<AuthIoContext> unitOfWork,
    IEmailProviderFactory emailProviderFactory) : IAuthenticationService
{
    private readonly ITokenService _tokenService = tokenService;

    private readonly IUnitOfWork<AuthIoContext>
        _unitOfWork = unitOfWork;

    private readonly IUserRepository _userRepository = userRepository;

    private readonly IEmailProvider emailProvider 
        = emailProviderFactory.GetSendGridEmailProvider();

    /// <summary>
    /// Método de registro de usuário.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="CreateUserFailedException"></exception>
    public async Task<ObjectResult> RegisterAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(AuthenticationService)} - METHOD {nameof(RegisterAsync)}\n");

        try
        {
            await new RegisterUserRequestValidator()
                .ValidateAsync(registerUserRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false) await validation.GetValidationErrors();

                    }).Unwrap();

            var transaction =
                await _unitOfWork.BeginTransactAsync();

            try
            {
                var userEntity
                    = registerUserRequest.ToUserSystemEntity();

                return await _userRepository
                    .CreateUserAsync(userEntity, registerUserRequest.Password).ContinueWith(async (identityResultTask) =>
                    {
                        var identityResult
                                = identityResultTask.Result;

                        if (identityResult.Succeeded is false)
                            throw new CreateUserFailedException(
                                registerUserRequest, identityResult.Errors.Select((e)
                                    => new DadosNotificacao(e.Description)).ToList());

                        return await _userRepository
                            .AddToUserRoleAsync(userEntity, "System").ContinueWith(async (identityResultTask) =>
                            {
                                var identityResult
                                        = identityResultTask.Result;

                                if (identityResult.Succeeded is false)
                                    throw new UserToRoleFailedException(
                                        registerUserRequest, identityResult.Errors.Select((e)
                                            => new DadosNotificacao(e.Description)).ToList());

                                await transaction.CommitAsync()
                                    .ContinueWith(async (task) => {

                                        await emailProvider.SendEmail(CreateDefaultEmailMessage
                                            .CreateWithHtmlContent("HYPER.IO", "hyper.io@outlook.com",
                                                userEntity.FirstName, userEntity.Email, "Confiormação de E-mail",
                                                "Confirme seu e-mail", "Confirmação de e-mail teste"));

                                    }).Unwrap();

                                return new OkObjectResult(
                                    new ApiResponse<UserResponse>(
                                        identityResult.Succeeded,
                                        HttpStatusCode.Created,
                                        userEntity.ToResponse(), [
                                            new DadosNotificacao("Usuário criado com sucesso!")]));
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

    /// <summary>
    /// Método responsável por fazer a authorização do usuário.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundUserException"></exception>
    public async Task<ObjectResult> AuthenticationAsync(LoginRequest loginRequest)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(AuthenticationService)} - METHOD {nameof(AuthenticationAsync)}\n");

        try
        {
            await new LoginRequestValidator().ValidateAsync(
                loginRequest).ContinueWith(async (validationTask) =>
                {
                    var validation = validationTask.Result;

                    if (validation.IsValid is false)
                        await validation.GetValidationErrors();

                }).Unwrap();

            return await _userRepository.GetWithUsernameAsync(
                loginRequest.Username).ContinueWith(async (userEntityTask) =>
                {
                    var userEntity =
                        userEntityTask.Result
                        ?? throw new NotFoundUserException(loginRequest);

                    await _userRepository.PasswordSignInAsync(
                        userEntity, loginRequest.Password, true, true).ContinueWith((signInResultTask) =>
                        {
                            var signInResult = signInResultTask.Result;

                            if (signInResult.Succeeded is false)
                                ThrownAuthorizationException(signInResult, userEntity.Id, loginRequest);
                        });

                    Log.Information(
                        $"[LOG INFORMATION] - Usuário autenticado com sucesso!\n");

                    return await GenerateTokenJwtAsync(loginRequest).ContinueWith(
                        (tokenJwtTask) =>
                        {
                            var tokenJWT =
                                tokenJwtTask.Result
                                ?? throw new TokenJwtException(null);

                            Log.Information(
                                $"[LOG INFORMATION] - Token gerado com sucesso {JsonConvert.SerializeObject(tokenJWT)}!\n");

                            return new OkObjectResult(
                                new ApiResponse<TokenJWT>(
                                    true, HttpStatusCode.Created, tokenJWT, [
                                        new("Token criado com sucesso!")]));
                        });

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception:{exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Método responsavel por gerar um tokenJwt.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    private async Task<TokenJWT> GenerateTokenJwtAsync(LoginRequest loginRequest)
        => await _tokenService.CreateJsonWebToken(loginRequest.Username).ContinueWith((tokenTask) =>
        {
            var tokenJwt =
                tokenTask.Result
                ?? throw new TokenJwtException(loginRequest);

            return tokenJwt;
        });

    /// <summary>
    /// Método responsável por tratar os erros de autenticação.
    /// </summary>
    /// <param name="signInResult"></param>
    /// <returns></returns>
    /// <exception cref="LockedOutAuthenticationException"></exception>
    /// <exception cref="IsNotAllowedAuthenticationException"></exception>
    /// <exception cref="RequiresTwoFactorAuthenticationException"></exception>
    /// <exception cref="InvalidUserAuthenticationException"></exception>
    private static void ThrownAuthorizationException(SignInResult signInResult, Guid userId, LoginRequest loginRequest)
    {
        if (signInResult.IsLockedOut)
        {
            Log.Information($"[LOG INFORMATION] - Falha ao autenticar usuário, está bloqueado.\n");

            throw new LockedOutAuthenticationException(loginRequest);
        }
        else if (signInResult.IsNotAllowed)
        {
            Log.Information($"[LOG INFORMATION] - Falha ao autenticar usuário, não está confirmado.\n");

            throw new IsNotAllowedAuthenticationException(new
            {
                userId,
                isNotAllowed = true,
                loginRequest
            });
        }
        else if (signInResult.RequiresTwoFactor)
        {
            Log.Information($"[LOG INFORMATION] - Falha ao autenticar usuário, requer verificação de dois fatores.\n");

            throw new RequiresTwoFactorAuthenticationException(loginRequest);
        }
        else
        {
            Log.Information($"[LOG INFORMATION] - Falha na autenticação dados incorretos!\n");

            throw new InvalidUserAuthenticationException(loginRequest);
        }
    }

}