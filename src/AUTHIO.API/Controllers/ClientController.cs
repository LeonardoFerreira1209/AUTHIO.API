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
/// Controller que cuida do fluxo de autenticação.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="Clientservice"></param>
[ApiController]
[ControllerName("Clients")]
[Route("api/Clients")]
public class ClientController(
    IFeatureFlagsService featureFlags,
    IClientservice Clientservice) : BaseController(featureFlags)
{
    private readonly IClientservice
        _Clientservice = Clientservice;

    /// <summary>
    /// Endpoint responsável pelo registro de Client.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Clients, "POST")]
    [SwaggerOperation(Summary = "Registrar Client", Description = "Método responsável por registrar um Client!")]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateClientRequest createClientRequest,
        CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("Controller", "ClientController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(createClientRequest)))
        using (LogContext.PushProperty("Metodo", "CreateAsync"))
        {
            return await ExecuteAsync(
                nameof(CreateAsync),
                () => _Clientservice.CreateAsync(
                    createClientRequest,
                    cancellationToken
                ),
                "Registrar Client.",
                cancellationToken
            );
        }
    }

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
        using (LogContext.PushProperty("Controller", "ClientController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(updateClientRequest)))
        using (LogContext.PushProperty("Metodo", "UpdateAsync"))
        {
            return await ExecuteAsync(
                nameof(UpdateAsync),
                () => _Clientservice.UpdateAsync(
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
        using (LogContext.PushProperty("Controller", "ClientController"))
        using (LogContext.PushProperty("Metodo", "CreateAsync"))
        {
            return await ExecuteAsync(
                nameof(GetAllAsync),
                () => _Clientservice.GetAllAsync(
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
    /// <param name="ClientKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("users/register")]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Clients, "PATCH")]
    [SwaggerOperation(
        Summary = "Registrar usuário no Client",
        Description = "Método responsável por registrar um usuário no Client!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterClientUserAsync(
        [FromHeader(Name = "x-Client-key")] string ClientKey,
        [FromBody] RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "ClientController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(registerUserRequest)))
        using (LogContext.PushProperty("Metodo", "RegisterClientUserAsync"))
        {
            return await ExecuteAsync(
                nameof(RegisterClientUserAsync),
                () => _Clientservice.RegisterClientUserAsync(
                    registerUserRequest,
                    ClientKey,
                    cancellationToken
                ),
                "Registrar usuário no Client.",
                cancellationToken
            );
        }
    }
}
