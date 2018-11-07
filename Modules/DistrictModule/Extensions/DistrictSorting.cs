using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.DistrictModule.Extensions
{
    public static class DistrictSorting
    {
        public static IQueryable<District> ApplySorting(this IQueryable<District> query, Filter filter)
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
