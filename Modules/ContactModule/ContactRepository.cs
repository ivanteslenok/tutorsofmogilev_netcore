using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.DTOs;
using Data.Entities;
using Data.ExtensionsDTO;
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

        public async Task<List<ContactDTO>> GetList()
        {
            return await _db.Contacts
                .Include(ct => ct.ContactType)
                .OrderBy(x => x.Id)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<ContactDTO>> GetList(ContactListFilter filter)
        {
            var query = _db.Contacts
                .Include(ct => ct.ContactType)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<ContactDTO>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<ContactDTO> GetItem(int? id)
        {
            var item = await _db.Contacts
                .Include(ct => ct.ContactType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return item.CreateDto();
        }

        public async Task<ContactDTO> InsertItem(Contact item)
        {
            _db.Contacts.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<ContactDTO> UpdateItem(Contact item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
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
