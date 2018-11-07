using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data.Entities;
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

        public async Task<List<Subject>> GetList()
        {
            return await _db.Subjects
                //.Include(s => s.Tutors)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<Subject>> GetList(SubjectListFilter filter)
        {
            var query = _db.Subjects
                //.Include(s => s.Tutors)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.Count();

            var result = await query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            return new ListModel<Subject>
            {
                Items = result,
                TotalCount = totalCount
            };
        }

        public async Task<Subject> GetItem(int? id)
        {
            return await _db.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Subject> InsertItem(Subject item)
        {
            _db.Subjects.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<Subject> UpdateItem(Subject item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
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
