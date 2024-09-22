namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Classe de Response paginado.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="items"></param>
/// <param name="totalCount"></param>
/// <param name="pageNumber"></param>
/// <param name="pageSize"></param>
public class PaginatedResponse<T>(
    IEnumerable<T> items, 
    int totalCount, 
    int pageNumber, 
    int pageSize
)
{
    /// <summary>
    /// Itens
    /// </summary>
    public IEnumerable<T> Items { get; set; } = items;

    /// <summary>
    /// Total de itens na base.
    /// </summary>
    public int TotalCount { get; set; } = totalCount;

    /// <summary>
    /// Numero da pagina.
    /// </summary>
    public int PageNumber { get; set; } = pageNumber;

    /// <summary>
    /// Tamanho da pagina.
    /// </summary>
    public int PageSize { get; set; } = pageSize;

    /// <summary>
    /// Converte o dado da Pagination response em outro. Ex.(Entity em Response).
    /// </summary>
    /// <typeparam name="Y"></typeparam>
    /// <param name=""></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public PaginatedResponse<Y> ConvertPaginationData<Y>(
        IList<Y> items)
    {
        return new PaginatedResponse<Y>(
            items, TotalCount, PageNumber, PageSize); ;
    }
}
