namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedResponse<T>
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="items"></param>
    /// <param name="totalCount"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    public PaginatedResponse(
        IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Itens
    /// </summary>
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    /// Total de itens na base.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Numero da pagina.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamanho da pagina.
    /// </summary>
    public int PageSize { get; set; }

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
