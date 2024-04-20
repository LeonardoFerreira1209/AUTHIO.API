using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;

namespace AUTHIO.APPLICATION.Domain.Exceptions.Base;

/// <summary>
/// Classe base de exceptions tratados.
/// </summary>
public abstract class BaseException : Exception
{
    /// <summary>
    /// ctor
    /// </summary>
    public BaseException() { }

    /// <summary>
    /// Response dos exceptiopns.
    /// </summary>
    public ErrorResponse Response { get; set; }
}
