using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Exceptions.Base;
using FluentValidation.Results;
using System.Net;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Extensão para o Validation Customizados.
/// </summary>
public static class CustomValidationExtensions
{
    /// <summary>
    /// Tratamentos de erros.
    /// </summary>
    /// <param name="validationResult"></param>
    /// <returns></returns>
    public static Task GetValidationErrors(this ValidationResult validationResult, object dados = null)
    {
        var notificacoes = new List<DataNotifications>();

        foreach (var error in validationResult.Errors) notificacoes.Add(new DataNotifications(error.ErrorMessage));

        throw new CustomException(HttpStatusCode.BadRequest, dados, notificacoes);
    }
}
