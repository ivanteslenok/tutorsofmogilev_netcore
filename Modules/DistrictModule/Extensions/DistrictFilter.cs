using System.Linq;
using Data.Entities;
using Modules.DistrictModule.Filters;

namespace Modules.DistrictModule.Extensions
{
    public static class DistrictFilter
    {
        public static IQueryable<District> ApplyFiltering(this IQueryable<District> query, DistrictListFilter filter)
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