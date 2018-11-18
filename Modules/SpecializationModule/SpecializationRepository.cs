using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.Entities;
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

        public async Task<List<Specialization>> GetList()
        {
            return await _db.Specializations
                //.Include(s => s.Tutors)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<Specialization>> GetList(SpecializationListFilter filter)
        {
            var query = _db.Specializations
                //.Include(s => s.Tutors)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<Specialization>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<Specialization> GetItem(int? id)
        {
            return await _db.Specializations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Specialization> InsertItem(Specialization item)
        {
            _db.Specializations.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<Specialization> UpdateItem(Specialization item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
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