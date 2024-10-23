using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers.OpenConnect;

/// <summary>
/// Controller que cuida do fluxo de open id connect.
/// </summary>
[ApiController]
public class OpenConnectController : ControllerBase
{
    /// <summary>
    ///  Endpoint responsável por buscar dados do open Id connect.
    /// </summary>
    /// <returns></returns>
    [HttpGet(".well-known/openid-configuration")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(
        Summary = "Busca dados do open id connect baseado no tenatKey.",
        Description = "Método responsável por retornar os dados do open id connect."
    )]
    public async Task<IActionResult> OpenConnectIdConfigurations(
        )
    {
        using (LogContext.PushProperty("Controller", "UserController"))
        using (LogContext.PushProperty("Metodo", "Signin"))
        {
            return Ok();
        }
    }
}
