using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.DOMAIN.Helpers.Extensions;
using FluentValidation;

namespace AUTHIO.DOMAIN.Validators;

/// <summary>
/// Classe de validação de classe de atualização de tenants no sistema.
/// </summary>
public sealed class UpdateTenantRequestValidator : AbstractValidator<UpdateTenantRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public UpdateTenantRequestValidator()
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
