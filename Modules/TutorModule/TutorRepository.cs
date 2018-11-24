using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Extensions;
using Core.Models;
using Data.DTOs;
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
        private readonly IMapper _mapper;

        public TutorRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<TutorDTO>> GetList()
        {
            var entities = await _db.Tutors
                .Include(x => x.Phones)
                .Include(x => x.Contacts)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.District)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<TutorDTO>>(entities);
        }

        public async Task<ListModel<TutorDTO>> GetList(TutorListFilter filter)
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

            return new ListModel<TutorDTO>
            {
                Items = _mapper.Map<List<TutorDTO>>(result.Result),
                TotalCount = totalCount.Result
            };
        }

        public async Task<TutorDTO> GetItem(int? id)
        {
            var item = await _db.Tutors
                .Include(x => x.Phones)
                .Include(x => x.Contacts)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.District)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<TutorDTO>(item);
        }

        public async Task<TutorDTO> InsertItem(Tutor item)
        {
            _db.Tutors.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<TutorDTO>(item) : null;
        }

        public async Task<TutorDTO> UpdateItem(Tutor item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<TutorDTO>(item) : null;
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