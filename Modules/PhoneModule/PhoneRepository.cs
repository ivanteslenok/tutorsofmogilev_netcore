using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.Entities;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Modules.PhoneModule.Extensions;
using Modules.PhoneModule.Filters;

namespace Modules.PhoneModule
{
    public class PhoneRepository
    {
        private readonly ApplicationContext _db;

        public PhoneRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<Phone>> GetList()
        {
            return await _db.Phones
                //.Include(ct => ct.Tutor)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<Phone>> GetList(PhoneListFilter filter)
        {
            var query = _db.Phones
                //.Include(ct => ct.Tutor)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<Phone>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<Phone> GetItem(int? id)
        {
            return await _db.Phones.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Phone> InsertItem(Phone item)
        {
            _db.Phones.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<Phone> UpdateItem(Phone item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new Phone { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}
