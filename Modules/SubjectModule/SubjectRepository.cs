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
using Modules.SubjectModule.Extensions;
using Modules.SubjectModule.Filters;

namespace Modules.SubjectModule
{
    public class SubjectRepository
    {
        private readonly ApplicationContext _db;

        public SubjectRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<SubjectDTO>> GetList()
        {
            return await _db.Subjects
                .OrderBy(x => x.Id)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<SubjectDTO>> GetList(SubjectListFilter filter)
        {
            var query = _db.Subjects
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .Select(x => x.CreateDto())
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<SubjectDTO>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<SubjectDTO> GetItem(int? id)
        {
            var item = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == id);

            return item.CreateDto();
        }

        public async Task<SubjectDTO> InsertItem(Subject item)
        {
            _db.Subjects.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<SubjectDTO> UpdateItem(Subject item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item.CreateDto() : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new Subject { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}
