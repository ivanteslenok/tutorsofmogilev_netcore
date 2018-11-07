using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.SubjectModule.Extensions
{
    public static class SubjectSorting
    {
        public static IQueryable<Subject> ApplySorting(this IQueryable<Subject> query, Filter filter)
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
