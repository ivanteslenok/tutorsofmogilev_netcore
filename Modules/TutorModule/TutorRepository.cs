using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Models;
using Data;
using Data.Entities;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Modules.TutorModule.Extensions;
using Modules.TutorModule.Filters;

namespace Modules.TutorModule
{
    public class TutorRepository
    {
        private readonly ApplicationContext _db;

        public TutorRepository(ApplicationContext applicationContext)
        {
            _db = applicationContext;
        }

        public async Task<List<Tutor>> GetList()
        {
            return await _db.Tutors
                .Include(x => x.Phones)
                .Include(x => x.Contacts)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.District)
                .Include(x => x.District)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ListModel<Tutor>> GetList(TutorListFilter filter)
        {
            var query = _db.Tutors
                .Include(x => x.Phones)
                .Include(x => x.Contacts)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.District)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<Tutor>
            {
                Items = result.Result,
                TotalCount = totalCount.Result
            };
        }

        public async Task<Tutor> GetItem(int? id)
        {
            return await _db.Tutors
                .Include(x => x.Phones)
                .Include(x => x.Contacts)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.District)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tutor> InsertItem(Tutor item)
        {
            _db.Tutors.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<Tutor> UpdateItem(Tutor item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? item : null;
        }

        public async Task<bool> DeleteItem(long id)
        {
            var item = new Tutor { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}