using System.Linq;
using Core.Models;
using Data.Entities;

namespace Modules.ContactModule.Extensions
{
    public static class ContactSorting
    {
        public static IQueryable<Contact> ApplySorting(this IQueryable<Contact> query, Filter filter)
        {
            var by = filter.SortBy?.ToUpper() ?? "ID";
            var direction = filter.SortDirection?.ToUpper() ?? "ASC";

            switch (by)
            {
                case "ID":
                    switch (direction)
                    {
                        case "ASC":
                            query = query.OrderBy(t => t.Id);
                        break;
                        case "DESC":
                            query = query.OrderByDescending(t => t.Id);
                        break;
                    }
                break;
                default:
                    query = query.OrderBy(t => t.Id);
                break;
            }

            return query;
        }
    }
}
