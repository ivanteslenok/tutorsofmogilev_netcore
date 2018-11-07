using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.SpecializationModule.Extensions
{
    public static class SpecializationSorting
    {
        public static IQueryable<Specialization> ApplySorting(this IQueryable<Specialization> query, Filter filter)
        {
            switch (filter.SortBy)
            {
                case "id":
                    query = filter.DescSort ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id);
                break;
                default:
                    query = filter.DescSort ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id);
                break;
            }

            return query;
        }
    }
}
