using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using FluentValidation;

namespace AUTHIO.APPLICATION.Domain.Validators;

/// <summary>
/// Classe de validação de classe de registro de tenants no sistema.
/// </summary>
public sealed class CreateTenantRequestValidator : AbstractValidator<CreateTenantRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public CreateTenantRequestValidator()
    {
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
