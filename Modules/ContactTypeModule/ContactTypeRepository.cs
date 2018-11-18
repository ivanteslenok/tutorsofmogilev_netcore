using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.Entities;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Modules.ContactTypeModule.Extensions;
using Modules.ContactTypeModule.Filters;

namespace Modules.ContactTypeModule
{
    public class ContactTypeRepository
    {
        private readonly ApplicationContext _db;

        public ContactTypeRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<ContactType>> GetList()
        {
            return await _db.ContactTypes
                //.Include(ct => ct.Contacts)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<ContactType>> GetList(ContactTypeListFilter filter)
        {
            var query = _db.ContactTypes
                //.Include(ct => ct.Contacts)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<ContactType>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<ContactType> GetItem(int? id)
        {
            return await _db.ContactTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ContactType> InsertItem(ContactType item)
        {
            _db.ContactTypes.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<ContactType> UpdateItem(ContactType item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new ContactType { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}