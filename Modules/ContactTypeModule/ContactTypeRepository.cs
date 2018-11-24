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
using Modules.ContactTypeModule.Extensions;
using Modules.ContactTypeModule.Filters;

namespace Modules.ContactTypeModule
{
    public class ContactTypeRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public ContactTypeRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<ContactTypeDTO>> GetList()
        {
            var entities = await _db.ContactTypes
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ContactTypeDTO>>(entities);
        }

        public async Task<ListModel<ContactTypeDTO>> GetList(ContactTypeListFilter filter)
        {
            var query = _db.ContactTypes
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<ContactTypeDTO>
            {
                Items = _mapper.Map<List<ContactTypeDTO>>(result.Result),
                TotalCount = totalCount.Result
            };
        }

        public async Task<ContactTypeDTO> GetItem(int? id)
        {
            var item = await _db.ContactTypes.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ContactTypeDTO>(item);
        }

        public async Task<ContactTypeDTO> InsertItem(ContactType item)
        {
            _db.ContactTypes.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<ContactTypeDTO>(item) : null;
        }

        public async Task<ContactTypeDTO> UpdateItem(ContactType item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<ContactTypeDTO>(item) : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new ContactType { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}