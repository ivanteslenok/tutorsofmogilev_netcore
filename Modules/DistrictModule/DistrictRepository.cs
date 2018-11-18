using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.Entities;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Modules.DistrictModule.Extensions;
using Modules.DistrictModule.Filters;

namespace Modules.DistrictModule
{
    public class DistrictRepository
    {
        private readonly ApplicationContext _db;

        public DistrictRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<District>> GetList()
        {
            return await _db.Districts
                //.Include(s => s.Tutors)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<District>> GetList(DistrictListFilter filter)
        {
            var query = _db.Districts
                //.Include(s => s.Tutors)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<District>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<District> GetItem(int? id)
        {
            return await _db.Districts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<District> InsertItem(District item)
        {
            _db.Districts.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<District> UpdateItem(District item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new District { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}