using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;
using FluentValidation;

namespace AUTHIO.APPLICATION.Domain.Validators;

/// <summary>
/// Classe de validação de classe de registro de usuários no sistema.
/// </summary>
public sealed class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public RegisterUserRequestValidator()
    {
        RuleFor(t => t.FirstName)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo nome.");

        RuleFor(t => t.LastName)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo sobrenome do usuário.");

        RuleFor(t => t.PhoneNumber)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo número de telefone do usuáerio.");

        RuleFor(t => t.Email)
           .NotEmpty()
           .NotNull()
           .EmailAddress().WithMessage("Preencha o e-mail em um formato correto!")
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo e-mail do usuário.");

        RuleFor(t => t.Password)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo senha do usuário.");

        RuleFor(t => t.UserName)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo Username.");
    }
}
