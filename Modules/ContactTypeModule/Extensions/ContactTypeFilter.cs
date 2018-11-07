using System.Linq;
using Data.Entities;
using Modules.ContactTypeModule.Filters;

namespace Modules.ContactTypeModule.Extensions
{
    public static class ContactTypeFilter
    {
        public static IQueryable<ContactType> ApplyFiltering(this IQueryable<ContactType> query, ContactTypeListFilter filter)
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