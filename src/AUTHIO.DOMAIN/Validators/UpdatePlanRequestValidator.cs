using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.DOMAIN.Helpers.Extensions;
using FluentValidation;

namespace AUTHIO.DOMAIN.Validators;

/// <summary>
/// Classe de validação de classe de atualização de planos no sistema.
/// </summary>
public sealed class UpdatePlanRequestValidator : AbstractValidator<UpdatePlanRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public UpdatePlanRequestValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo Id.");

        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo nome.");

        RuleFor(p => p.Description)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo descrição.");

        RuleFor(p => p.QuantTenants)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo quantidade de tenants.");

        RuleFor(p => p.QuantUsers)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
           .WithMessage("Preencha o campo quantidade de usuarios.");

        RuleFor(p => p.Value)
          .NotEmpty()
          .NotNull()
          .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
          .WithMessage("Preencha o campo valor.");
    }
}
