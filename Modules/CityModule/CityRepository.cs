using AutoMapper;
using Core.Extensions;
using Core.Models;
using Data.DTOs;
using Data.Entities;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using Modules.CityModule.Extensions;
using Modules.CityModule.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modules.CityModule
{
    public class CityRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public CityRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<CityDTO>> GetList()
        {
            var entities = await _db.Cities
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<CityDTO>>(entities);
        }

        public async Task<ListModel<CityDTO>> GetList(CityListFilter filter)
        {
            var query = _db.Cities
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = await query.CountAsync();
            var result = await query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            return new ListModel<CityDTO>
            {
                Items = _mapper.Map<List<CityDTO>>(result),
                TotalCount = totalCount
            };
        }

        public async Task<CityDTO> GetItem(int? id)
        {
            var item = await _db.Cities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<CityDTO>(item);
        }

        public async Task<CityDTO> InsertItem(City item)
        {
            _db.Cities.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<CityDTO>(item) : null;
        }

        public async Task<CityDTO> UpdateItem(City item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<CityDTO>(item) : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new City { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}
