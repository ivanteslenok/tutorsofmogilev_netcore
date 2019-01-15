using Data.Entities;
using Modules.CityModule.Filters;
using System.Linq;

namespace Modules.CityModule.Extensions
{
    public static class CityFilter
    {
        public static IQueryable<City> ApplyFiltering(this IQueryable<City> query, CityListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x =>
                    !string.IsNullOrWhiteSpace(x.Name)
                    && x.Name.Contains(filter.Name)
                );
            }

            return query;
        }
    }
}
