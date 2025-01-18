using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Helpers.Expressions;

/// <summary>
/// Classe de extensão de expressões.
/// </summary>
public static class CustomLambdaExpressions
{
    /// <summary>
    /// Uni duas expressões com Or.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter = 
            Expression.Parameter(typeof(T), "x");

        var leftVisitor = 
            new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);

        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = 
            new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);

        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(left, right), parameter);
    }

    /// <summary>
    /// Uni duas expressões com And.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="expr1"></param>
    /// <param name="expr2"></param>
    /// <returns></returns>
    public static Expression<Func<T, bool>> And<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter =
            Expression.Parameter(typeof(T), "x");

        var leftVisitor =
            new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);

        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor =
            new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);

        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(
            Expression.And(left, right), parameter);
    }

    /// <summary>
    /// Classe de replace de expressão.
    /// </summary>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    internal class ReplaceExpressionVisitor(
        Expression oldValue, Expression newValue) : ExpressionVisitor
    {
        private readonly Expression _oldValue = oldValue;
        private readonly Expression _newValue = newValue;

        /// <summary>
        /// Método de vista.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
