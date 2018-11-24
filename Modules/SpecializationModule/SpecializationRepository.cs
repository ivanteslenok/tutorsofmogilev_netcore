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
using Modules.SpecializationModule.Extensions;
using Modules.SpecializationModule.Filters;

namespace Modules.SpecializationModule
{
    public class SpecializationRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public SpecializationRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<SpecializationDTO>> GetList()
        {
            var entities = await _db.Specializations
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<SpecializationDTO>>(entities);
        }

        public async Task<ListModel<SpecializationDTO>> GetList(SpecializationListFilter filter)
        {
            var query = _db.Specializations
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<SpecializationDTO>
            {
                Items = _mapper.Map<List<SpecializationDTO>>(result.Result),
                TotalCount = totalCount.Result
            };
        }

        public async Task<SpecializationDTO> GetItem(int? id)
        {
            var item = await _db.Specializations.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<SpecializationDTO>(item);
        }

        public async Task<SpecializationDTO> InsertItem(Specialization item)
        {
            _db.Specializations.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<SpecializationDTO>(item) : null;
        }

        public async Task<SpecializationDTO> UpdateItem(Specialization item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<SpecializationDTO>(item) : null;
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