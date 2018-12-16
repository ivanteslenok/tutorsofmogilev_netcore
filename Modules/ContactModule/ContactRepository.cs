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
using Modules.ContactModule.Extensions;
using Modules.ContactModule.Filters;

namespace Modules.ContactModule
{
    public class ContactRepository
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public ContactRepository(ApplicationContext applicationContext, IMapper mapper)
        {
            _db = applicationContext;
            _mapper = mapper;
        }

        public async Task<List<ContactDTO>> GetList()
        {
            var entities = await _db.Contacts
                .Include(ct => ct.ContactType)
                .OrderBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ContactDTO>>(entities);
        }

        public async Task<ListModel<ContactDTO>> GetList(ContactListFilter filter)
        {
            var query = _db.Contacts
                .Include(ct => ct.ContactType)
                .ApplyFiltering(filter)
                .ApplySorting(filter);

            var totalCount = query.CountAsync();

            var result = query
                .ApplyPaging(filter)
                .AsNoTracking()
                .ToListAsync();

            await Task.WhenAll(totalCount, result);

            return new ListModel<ContactDTO>
            {
                Items = _mapper.Map<List<ContactDTO>>(result.Result),
                TotalCount = totalCount.Result
            };
        }

        public async Task<ContactDTO> GetItem(int? id)
        {
            var item = await _db.Contacts
                .Include(ct => ct.ContactType)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ContactDTO>(item);
        }

        public async Task<ContactDTO> InsertItem(Contact item)
        {
            if (item.ContactType != null)
                _db.ContactTypes.Attach(item.ContactType);

            _db.Contacts.Add(item);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<ContactDTO>(item) : null;
        }

        public async Task<ContactDTO> UpdateItem(Contact item)
        {
            _db.Entry(item).State = EntityState.Modified;
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<ContactDTO>(item) : null;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = new Contact { Id = id };

            _db.Entry(item).State = EntityState.Deleted;
            int i = await _db.SaveChangesAsync();

            return i > 0;
        }
    }
}
