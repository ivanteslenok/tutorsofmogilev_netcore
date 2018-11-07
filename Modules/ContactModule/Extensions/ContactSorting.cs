using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.ContactModule.Extensions
{
    public static class ContactSorting
    {
        public static IQueryable<Contact> ApplySorting(this IQueryable<Contact> query, Filter filter)
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
