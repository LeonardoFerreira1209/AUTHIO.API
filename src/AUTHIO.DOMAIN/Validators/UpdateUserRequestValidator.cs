using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.DOMAIN.Helpers.Extensions;
using FluentValidation;

namespace AUTHIO.DOMAIN.Validators;

/// <summary>
/// Classe de validação de classe de atualização de usuários no sistema.
/// </summary>
public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public UpdateUserRequestValidator()
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
    }
}
