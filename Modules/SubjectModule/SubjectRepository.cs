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
using Modules.SubjectModule.Extensions;
using Modules.SubjectModule.Filters;

namespace Modules.SubjectModule
{
    public class SubjectRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public SubjectRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<SubjectDTO>> GetList()
        {
            var entities = await _db.Subjects
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<SubjectDTO>>(entities);
        }

        public async Task<ListModel<SubjectDTO>> GetList(SubjectListFilter filter)
        {
            var query = _db.Subjects
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Tutor)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = await query.CountAsync();
            var result = await query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            return new ListModel<SubjectDTO>
            {
                Items = _mapper.Map<List<SubjectDTO>>(result),
                TotalCount = totalCount
            };
        }

        public async Task<SubjectDTO> GetItem(int? id)
        {
            var item = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<SubjectDTO>(item);
        }

        public async Task<SubjectDTO> InsertItem(Subject item)
        {
            _db.Subjects.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<SubjectDTO>(item) : null;
        }

        public async Task<SubjectDTO> UpdateItem(Subject item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<SubjectDTO>(item) : null;
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
