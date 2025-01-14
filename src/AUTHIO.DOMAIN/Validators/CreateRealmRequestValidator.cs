﻿using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.DOMAIN.Helpers.Extensions;
using FluentValidation;

namespace AUTHIO.DOMAIN.Validators;

/// <summary>
/// Classe de validação de classe de registro de Realms no sistema.
/// </summary>
public sealed class CreateRealmRequestValidator : AbstractValidator<CreateRealmRequest>
{
    /// <summary>
    /// ctor
    /// </summary>
    public CreateRealmRequestValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo nome.");

        RuleFor(t => t.Email)
           .NotEmpty()
           .NotNull()
           .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
           .WithMessage("Preencha o campo email.");

        RuleFor(t => t.Description)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(ErrorCode.ErroInesperado.ToCode())
            .WithMessage("Preencha o campo descrição.");
    }
}
