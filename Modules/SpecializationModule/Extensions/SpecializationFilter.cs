using System.Linq;
using Data.Entities;
using Modules.SpecializationModule.Filters;

namespace Modules.SpecializationModule.Extensions
{
    public static class SpecializationFilter
    {
        public static IQueryable<Specialization> ApplyFiltering(this IQueryable<Specialization> query, SpecializationListFilter filter)
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