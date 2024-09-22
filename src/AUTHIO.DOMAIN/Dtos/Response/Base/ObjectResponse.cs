using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Objeto de retorno.
/// </summary>
public class ObjectResponse : ObjectResult
{
    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="value"></param>
    public ObjectResponse(
        HttpStatusCode statusCode,
        object value) : base(value)
    {
        StatusCode = (int)statusCode;
    }
}