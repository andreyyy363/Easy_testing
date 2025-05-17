using EasyTesting.Core.Models.Filter;

namespace EasyTesting.Core.Utils
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            return query.Skip(parameters.skip).Take(parameters.limit);
        }
    }

}
