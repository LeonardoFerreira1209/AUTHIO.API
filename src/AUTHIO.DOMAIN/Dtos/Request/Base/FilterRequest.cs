namespace AUTHIO.DOMAIN.Dtos.Request.Base;

/// <summary>
/// Classe Request de filtro.
/// </summary>
public class FilterRequest : IFilterRequest
{
    /// <summary>
    /// Número da página.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamanho da página.
    /// </summary>
    public int PageSize { get; set; }
}
