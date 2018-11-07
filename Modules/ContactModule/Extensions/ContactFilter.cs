using System.Linq;
using Data.Entities;
using Modules.ContactModule.Filters;

namespace Modules.ContactModule.Extensions
{
    public static class ContactFilter
    {
        public static IQueryable<Contact> ApplyFiltering(this IQueryable<Contact> query, ContactListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Name)
                    && x.Name.Contains(filter.Name)
                    );
            }

            if (!string.IsNullOrWhiteSpace(filter.Value))
            {
                query = query.Where(x => 
                    x.Value.Contains(filter.Value)
                );
            }

            if (filter.ContactTypeId != null)
            {
                query = query.Where(x => 
                    x.ContactTypeId == filter.ContactTypeId
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