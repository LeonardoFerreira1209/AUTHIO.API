using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Busca dados por Id e x-Client-key.
/// </summary>
public class IdWithXClientKey
{
    /// <summary>
    /// Id de referência.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Chave do Client.
    /// </summary>
    [FromHeader(Name = "x-Client-key")]
    public string ClientKey { get; set; }
}
