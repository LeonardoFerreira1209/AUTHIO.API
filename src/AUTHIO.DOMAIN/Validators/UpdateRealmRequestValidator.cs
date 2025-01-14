using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.DOMAIN.Helpers.Extensions;
using FluentValidation;

namespace AUTHIO.DOMAIN.Validators;

/// <summary>
/// Classe de validação de classe de atualização de Realm no sistema.
/// </summary>
public sealed class UpdateRealmRequestValidator : AbstractValidator<UpdateRealmRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public UpdateRealmRequestValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo Id.");

        RuleFor(t => t.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo nome.");

        RuleFor(t => t.Description)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo descrição.");
    }
}
