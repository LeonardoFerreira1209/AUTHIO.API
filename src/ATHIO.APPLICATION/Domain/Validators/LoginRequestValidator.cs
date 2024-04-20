using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST;
using FluentValidation;

namespace AUTHIO.APPLICATION.DOMAIN.VALIDATORS;

/// <summary>
/// Classe de validação de request de login.
/// </summary>
public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    // <summary>
    /// ctor
    /// </summary>
    public LoginRequestValidator()
    {
        RuleFor(t => t.Username)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo username.");

        RuleFor(t => t.Password)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo password.");
    }
}
