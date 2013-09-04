namespace Xoqal.Utilities.Linq
{
    using System;
    using System.Linq.Expressions;

    public static class LambdaExpressions
    {
        /// <summary>
        /// Gets property name of an expression
        /// </summary>
        /// <typeparam name="TSource">the source type to extract property name</typeparam>
        /// <typeparam name="TField">the field type of the expected property</typeparam>
        /// <param name="field">the expression to extract property name</param>
        /// <returns>indicated property name</returns>
        /// <example>
        /// Expression<Func<IPaginated, object>> rowCountProperty = arg => arg.TotalRowsCount;
        /// string pNamed = GetPropertyName<IPaginated, object>(rowCountProperty);
        /// </example>
        public static string GetPropertyName<TSource, TField>(Expression<Func<TSource, TField>> field)
        {
            return
                (field.Body as MemberExpression ??
                 ((UnaryExpression)field.Body).Operand as MemberExpression).Member
                                                                            .Name;
        }
    }
}
