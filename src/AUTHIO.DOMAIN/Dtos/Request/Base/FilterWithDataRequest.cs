namespace AUTHIO.DOMAIN.Dtos.Request.Base;

/// <summary>
/// Classe Request de filtro com dados por parametro.
/// </summary>
public class FilterWithDataRequest<T> : IFilterRequest
{
    /// <summary>
    /// Número da página.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamanho da página.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Dados de request.
    /// </summary>
    public T Data { get; set; } = default;
}
