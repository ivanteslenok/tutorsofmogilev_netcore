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
using Modules.SpecializationModule.Extensions;
using Modules.SpecializationModule.Filters;

namespace Modules.SpecializationModule
{
    public class SpecializationRepository
    {
        private readonly ApplicationContext _db;

        public SpecializationRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<SpecializationDTO>> GetList()
        {
            return await _db.Specializations
                .OrderBy(x => x.Id)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<SpecializationDTO>> GetList(SpecializationListFilter filter)
        {
            var query = _db.Specializations
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<SpecializationDTO>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<SpecializationDTO> GetItem(int? id)
        {
            var item = await _db.Specializations.FirstOrDefaultAsync(x => x.Id == id);

            return item.CreateDto();
        }

        public async Task<SpecializationDTO> InsertItem(Specialization item)
        {
            _db.Specializations.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<SpecializationDTO> UpdateItem(Specialization item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new Specialization { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}