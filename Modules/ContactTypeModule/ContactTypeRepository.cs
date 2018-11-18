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

        public async Task<List<ContactTypeDTO>> GetList()
        {
            return await _db.ContactTypes
                .OrderBy(x => x.Id)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<ContactTypeDTO>> GetList(ContactTypeListFilter filter)
        {
            var query = _db.ContactTypes
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<ContactTypeDTO>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<ContactTypeDTO> GetItem(int? id)
        {
            var item = await _db.ContactTypes.FirstOrDefaultAsync(x => x.Id == id);

            return item.CreateDto();
        }

        public async Task<ContactTypeDTO> InsertItem(ContactType item)
        {
            _db.ContactTypes.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<ContactTypeDTO> UpdateItem(ContactType item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
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