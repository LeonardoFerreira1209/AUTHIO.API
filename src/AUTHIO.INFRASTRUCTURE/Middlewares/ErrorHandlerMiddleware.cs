using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace AUTHIO.INFRASTRUCTURE.Middlewares;

/// <summary>
/// Middleware de erros.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="requestDelegate"></param>
public class ErrorHandlerMiddleware(
    RequestDelegate requestDelegate)
{
    private readonly RequestDelegate _requestDelegate = requestDelegate;

    /// <summary>
    /// Método de invocação de Handler.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Método de tratamento de response.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    /// <returns></returns>
    protected static Task HandleExceptionAsync(
        HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, json) =
            GenerateResponse(exception);

        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(json);
    }

    /// <summary>
    /// Método que gera o response baseado no tipo.
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    private static (HttpStatusCode statusCode, string json) GenerateResponse(Exception exception)
        => exception switch
        {
            BaseException customEx => (customEx.Response.StatusCode,
                                                           JsonSerializer.Serialize(customEx.Response)),
            _ => (HttpStatusCode.InternalServerError, JsonSerializer.Serialize(new
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Sucesso = false,
                Dados = new
                {
                    exception.StackTrace,
                    exception.Message,
                    InnerException = exception.InnerException?.ToString(),
                    exception.Data
                },
                Notificacoes = new DadosNotificacao(exception.Message),
            }))
        };
}
