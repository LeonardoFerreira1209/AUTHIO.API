using AUTHIO.DOMAIN.Entities;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Helpers.Expressions.Filters;

/// <summary>
/// Filtro de query baseada no Client entity.
/// </summary>
public static class ClientFilters
{
    /// <summary>
    /// Filtrar Client por user admin.
    /// </summary>
    /// <param name="adminId"></param>
    /// <returns></returns>
    public static Expression<Func<ClientEntity, bool>> FilterByAdmin(Guid adminId)
        => entidade => entidade.UserId == adminId;
}

