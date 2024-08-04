namespace AUTHIO.DOMAIN.Dtos.Request.Base;

/// <summary>
/// Interface de filtro.
/// </summary>
public interface IFilterRequest
{
    /// <summary>
    /// Número da página.
    /// </summary>
    int PageNumber { get; set; }

    /// <summary>
    /// Tamanho da página.
    /// </summary>
    int PageSize { get; set; }
}
