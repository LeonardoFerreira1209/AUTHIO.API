using AUTHIO.DOMAIN.Entities;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Helpers.Expressions.Filters;

/// <summary>
/// Filtro de query baseada no Realm entity.
/// </summary>
public static class RealmFilters
{
    /// <summary>
    /// Filtrar Realm por user admin.
    /// </summary>
    /// <param name="adminId"></param>
    /// <returns></returns>
    public static Expression<Func<RealmEntity, bool>> FilterByAdmin(Guid adminId)
        => entidade => entidade.UserId == adminId;
}

