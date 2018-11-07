using System.Linq;
using Data.Entities;
using Modules.SubjectModule.Filters;

namespace Modules.SubjectModule.Extensions
{
    public static class SubjectFilter
    {
        public static IQueryable<Subject> ApplyFiltering(this IQueryable<Subject> query, SubjectListFilter filter)
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