using System.Linq;
using Data.Entities;
using Modules.PhoneModule.Filters;

namespace Modules.PhoneModule.Extensions
{
    public static class PhoneFilter
    {
        public static IQueryable<Phone> ApplyFiltering(this IQueryable<Phone> query, PhoneListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Number))
            {
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Number)
                    && x.Number.Contains(filter.Number)
                );
            }

            if (!string.IsNullOrWhiteSpace(filter.Operator))
            {
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Operator)
                    && x.Operator.Contains(filter.Operator)
                );
            }

            if (filter.TutorId != null)
            {
                query = query.Where(x => 
                    x.TutorId == filter.TutorId
                );
            }

            return query;
        }
    }
}