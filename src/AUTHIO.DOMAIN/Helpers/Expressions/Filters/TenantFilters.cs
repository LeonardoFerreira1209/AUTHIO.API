using AUTHIO.DOMAIN.Entities;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Helpers.Expressions.Filters;

/// <summary>
/// Filtro de query baseada no Tenant entity.
/// </summary>
public static class TenantFilters
{
    /// <summary>
    /// Filtrar tenant por user admin.
    /// </summary>
    /// <param name="adminId"></param>
    /// <returns></returns>
    public static Expression<Func<TenantEntity, bool>> FilterByAdmin(Guid adminId)
        => entidade => entidade.UserId == adminId;
}

