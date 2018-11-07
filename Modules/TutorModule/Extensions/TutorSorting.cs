using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.TutorModule.Extensions
{
    public static class TutorSorting
    {
        public static IQueryable<Tutor> ApplySorting(this IQueryable<Tutor> query, Filter filter)
        {
            switch (filter.SortBy)
            {
                case "id":
                    query = filter.DescSort ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id);
                break;
                case "rating":
                    query = filter.DescSort ? query.OrderByDescending(t => t.Rating) : query.OrderBy(t => t.Rating);
                    break;
                default:
                    query = filter.DescSort ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id);
                break;
            }

            return query;
        }
    }
}
