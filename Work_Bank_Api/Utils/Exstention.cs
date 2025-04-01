using System.Linq.Expressions;

namespace Work_Bank_Api.Utils
{
    public static class Exstention
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> filter)
        {
            return condition ? source.Where(filter) : source;
        }
    }
}
