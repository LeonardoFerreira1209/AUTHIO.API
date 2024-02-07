using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using FluentValidation;

namespace AUTHIO.APPLICATION.Domain.Validators;

/// <summary>
/// Classe de validação de Tenant provision.
/// </summary>
public sealed class TenantProvisionRequestValidator : AbstractValidator<TenantProvisionRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantProvisionRequestValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo nome.");

        RuleFor(t => t.Description)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
            .WithMessage("Preencha o campo descrição.");

        RuleFor(t => t.UserAdmin.Name)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo nome de usuário.");

        RuleFor(t => t.UserAdmin.LastName)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo sobrenome do usuário.");

        RuleFor(t => t.UserAdmin.PhoneNumber)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo número de telefone do usuáerio.");

        RuleFor(t => t.UserAdmin.Email)
           .NotEmpty()
           .NotNull()
           .EmailAddress().WithMessage("Preencha o e-mail em um formato correto!")
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo e-mail do usuário.");

        RuleFor(t => t.UserAdmin.Password)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo senha do usuário.");

        RuleFor(t => t.UserAdmin.Username)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.CamposObrigatorios.ToCode())
           .WithMessage("Preencha o campo Username.");
    }
}
