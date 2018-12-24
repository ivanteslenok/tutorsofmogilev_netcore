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
using Modules.DistrictModule.Extensions;
using Modules.DistrictModule.Filters;

namespace Modules.DistrictModule
{
    public class DistrictRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public DistrictRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<DistrictDTO>> GetList()
        {
            var entities = await _db.Districts
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<DistrictDTO>>(entities);
        }

        public async Task<ListModel<DistrictDTO>> GetList(DistrictListFilter filter)
        {
            var query = _db.Districts
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<DistrictDTO>
            {
                Items = _mapper.Map<List<DistrictDTO>>(result.Result),
                TotalCount = totalCount.Result
            };
        }

        public async Task<DistrictDTO> GetItem(int? id)
        {
            var item = await _db.Districts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<DistrictDTO>(item);
        }

        public async Task<DistrictDTO> InsertItem(District item)
        {
            _db.Districts.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<DistrictDTO>(item) : null;
        }

        public async Task<DistrictDTO> UpdateItem(District item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<DistrictDTO>(item) : null;
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