using System.Linq.Expressions;
using WebCoreSongs.DLib;

namespace WebCoreSongs.DLib
{
    // from https://stackoverflow.com/questions/2032158/sorting-a-table-in-asp-net-mvc (Sorting a table in asp.net MVC):
    static class OrderByExtender
    {
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> collection, string key, string direction)
        {
            LambdaExpression sortLambda = BuildLambda<T>(key);

            if (direction.ToUpper() == "ASC")
                return collection.OrderBy((Func<T, object>)sortLambda.Compile());
            else
                return collection.OrderByDescending((Func<T, object>)sortLambda.Compile());
        }

        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> collection, string key, string direction)
        {
            LambdaExpression sortLambda = BuildLambda<T>(key);

            if (direction.ToUpper() == "ASC")
                return collection.ThenBy((Func<T, object>)sortLambda.Compile());
            else
                return collection.ThenByDescending((Func<T, object>)sortLambda.Compile());
        }

        private static LambdaExpression BuildLambda<T>(string key)
        {
            ParameterExpression TParameterExpression = Expression.Parameter(typeof(T), "p");
            LambdaExpression sortLambda = Expression.Lambda(Expression.Convert(Expression.Property(TParameterExpression, key), typeof(object)), TParameterExpression);
            return sortLambda;
        }
    }
}
