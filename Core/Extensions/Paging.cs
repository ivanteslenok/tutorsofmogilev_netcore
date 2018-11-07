using System.Linq;
using Core.Models;

namespace Core.Extensions
{
    public static class Paging
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter.PageNumber < 1)
            {
                filter.PageNumber = 1;
            }

            if (filter.PageSize == 0)
            {
                filter.PageSize = int.MaxValue;
            }

            return query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }
    }
}
