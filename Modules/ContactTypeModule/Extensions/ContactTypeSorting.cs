using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.ContactTypeModule.Extensions
{
    public static class ContactTypeSorting
    {
        public static IQueryable<ContactType> ApplySorting(this IQueryable<ContactType> query, Filter filter)
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
