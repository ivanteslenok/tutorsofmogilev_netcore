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

        public async Task<List<DistrictDTO>> GetList()
        {
            return await _db.Districts
                .OrderBy(x => x.Id)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<DistrictDTO>> GetList(DistrictListFilter filter)
        {
            var query = _db.Districts
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<DistrictDTO>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<DistrictDTO> GetItem(int? id)
        {
            var item = await _db.Districts.FirstOrDefaultAsync(x => x.Id == id);

            return item.CreateDto();
        }

        public async Task<DistrictDTO> InsertItem(District item)
        {
            _db.Districts.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<DistrictDTO> UpdateItem(District item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
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