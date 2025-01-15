using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.CustomAuthorize.Attribute;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Controller que cuida do fluxo de cliente.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="clientservice"></param>
[ApiController]
[ControllerName("Clients")]
[Route("api/clients")]
public class ClientController(
    IFeatureFlagsService featureFlags,
    IClientservice clientservice) : BaseController(featureFlags)
{
    private readonly IClientservice
        _clientservice = clientservice;

    /// <summary>
    /// Endpoint responsável pela atualização de um Client.
    /// </summary>
    /// <param name="updateClientRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Clients, "PUT")]
    [SwaggerOperation(
        Summary = "Atualizar Client",
        Description = "Método responsável por atualizar um Client!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateClientRequest updateClientRequest,
        CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("Controller", nameof(ClientController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(updateClientRequest)))
        using (LogContext.PushProperty("Metodo", nameof(UpdateAsync)))
        {
            return await ExecuteAsync(
                nameof(UpdateAsync),
                () => _clientservice.UpdateAsync(
                    updateClientRequest,
                    cancellationToken
                ),
                "Atualizar Client.",
                cancellationToken
            );
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar os Clients do usuário logado.
    /// </summary>
    /// <param name="filterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Clients, "GET")]
    [SwaggerOperation(
        Summary = "Buscar todos os Clients",
        Description = "Método responsável por buscar todos os Clients do usuário!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(
           [FromQuery] FilterRequest filterRequest,
           CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("Controller", nameof(ClientController)))
        using (LogContext.PushProperty("Metodo", nameof(GetAllAsync)))
        {
            return await ExecuteAsync(
                nameof(GetAllAsync),
                () => _clientservice.GetAllAsync(
                    filterRequest,
                    cancellationToken
                ),
                "Buscar todos os Clients.",
                cancellationToken
            );
        }
    }

    /// <summary>
    /// Endpoint responsável pelo registro de usuário em um Client.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="clientKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{x-client-key}/users/register")]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Clients, "POST")]
    [SwaggerOperation(
        Summary = "Registrar usuário no Client",
        Description = "Método responsável por registrar um usuário no Client!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterClientUserAsync(
        [FromRoute(Name = "x-client-key")] string clientKey,
        [FromBody] RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", nameof(ClientController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(registerUserRequest)))
        using (LogContext.PushProperty("Metodo", nameof(RegisterClientUserAsync)))
        {
            return await ExecuteAsync(
                nameof(RegisterClientUserAsync),
                () => _clientservice.RegisterClientUserAsync(
                    registerUserRequest,
                    clientKey,
                    cancellationToken
                ),
                "Registrar usuário no Client.",
                cancellationToken
            );
        }
    }
}
