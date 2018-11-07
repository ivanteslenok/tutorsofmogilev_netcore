using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.PhoneModule.Extensions
{
    public static class PhoneSorting
    {
        public static IQueryable<Phone> ApplySorting(this IQueryable<Phone> query, Filter filter)
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
