using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.Entities;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Modules.ContactModule.Extensions;
using Modules.ContactModule.Filters;

namespace Modules.ContactModule
{
    public class ContactRepository
    {
        private readonly ApplicationContext _db;

        public ContactRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<Contact>> GetList()
        {
            return await _db.Contacts
                .Include(ct => ct.ContactType)
                //.Include(ct => ct.Tutor)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<Contact>> GetList(ContactListFilter filter)
        {
            var query = _db.Contacts
                .Include(ct => ct.ContactType)
                //.Include(ct => ct.Tutor)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<Contact>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<Contact> GetItem(int? id)
        {
            return await _db.Contacts
                .Include(ct => ct.ContactType)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Contact> InsertItem(Contact item)
        {
            _db.Contacts.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<Contact> UpdateItem(Contact item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new Contact { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}
