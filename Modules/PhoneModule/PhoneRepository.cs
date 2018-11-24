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
using Modules.PhoneModule.Extensions;
using Modules.PhoneModule.Filters;

namespace Modules.PhoneModule
{
    public class PhoneRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public PhoneRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<PhoneDTO>> GetList()
        {
            var entities = await _db.Phones
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PhoneDTO>>(entities);
        }

        public async Task<ListModel<PhoneDTO>> GetList(PhoneListFilter filter)
        {
            var query = _db.Phones
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<PhoneDTO>
            {
                Items = _mapper.Map<List<PhoneDTO>>(result.Result),
                TotalCount = totalCount.Result
            };
        }

        public async Task<PhoneDTO> GetItem(int? id)
        {
            var item = await _db.Phones.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<PhoneDTO>(item);
        }

        public async Task<PhoneDTO> InsertItem(Phone item)
        {
            _db.Phones.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<PhoneDTO>(item) : null;
        }

        public async Task<PhoneDTO> UpdateItem(Phone item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<PhoneDTO>(item) : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new Phone { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}
