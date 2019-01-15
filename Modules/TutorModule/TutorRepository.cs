using System;
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
                    .ThenInclude(x => x.ContactType)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.City)
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
                    .ThenInclude(x => x.ContactType)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.City)
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

        public async Task<TutorDTO> GetItem(long? id)
        {
            var item = await _db.Tutors
                .Include(x => x.Phones)
                .Include(x => x.Contacts)
                .Include(x => x.TutorSpecializations)
                    .ThenInclude(x => x.Specialization)
                .Include(x => x.TutorSubjects)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.City)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<TutorDTO>(item);
        }

        public async Task<TutorDTO> InsertItem(TutorDTO itemDTO)
        {
            var itemForSave = _mapper.Map<Tutor>(itemDTO);
            itemForSave.CreateDate = DateTime.Now;
            
            _db.Cities.Attach(itemForSave.City);
            _db.Tutors.Add(itemForSave);
            int i = await _db.SaveChangesAsync();

            return i > 0 ? _mapper.Map<TutorDTO>(itemForSave) : null;
        }

        public async Task<TutorDTO> UpdateItem(TutorDTO item)
        {
            var itemForSave = _mapper.Map<Tutor>(item);

            _db.Tutors.Attach(itemForSave);

            _db.Entry(itemForSave).Property(x => x.FirstName).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.LastName).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Patronymic).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Education).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Job).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Address).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Rating).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.IsVisible).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Cost).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Description).IsModified = true;
            _db.Entry(itemForSave).Property(x => x.Experience).IsModified = true;
            _db.Entry(itemForSave).Reference(x => x.City).IsModified = true;

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

        public async Task<bool> UpdateTutorSubjects(long id, long[] added, long[] deleted)
        {
            var tutor = _db.Tutors
                .Include(x => x.TutorSubjects)
                .First(x => x.Id == id);

            tutor.TutorSubjects
                .Where(x => deleted.Contains(x.SubjectId))
                .ToList()
                .ForEach(x => tutor.TutorSubjects.Remove(x));

            var addedSubjects = _db.Subjects.Where(x => added.Contains(x.Id));

            foreach (var item in addedSubjects)
                tutor.TutorSubjects
                    .Add(new TutorSubject 
                    { 
                        Tutor = tutor, 
                        Subject = item 
                    });

            int i = await _db.SaveChangesAsync();

            return i > 0;
        }

        public async Task<bool> UpdateTutorSpecializations(long id, long[] added, long[] deleted)
        {
            var tutor = _db.Tutors
                .Include(x => x.TutorSpecializations)
                .First(x => x.Id == id);

            tutor.TutorSpecializations
                .Where(x => deleted.Contains(x.SpecializationId))
                .ToList()
                .ForEach(x => tutor.TutorSpecializations.Remove(x));

            var addedSpecializations = _db.Specializations.Where(x => added.Contains(x.Id));

            foreach (var item in addedSpecializations)
                tutor.TutorSpecializations
                    .Add(new TutorSpecialization 
                    { 
                        Tutor = tutor,
                        Specialization = item 
                    });

            int i = await _db.SaveChangesAsync();

            return i > 0;
        }

        public async Task SetPhotoPath(long id, string photoName)
        {
            var tutor = await _db.Tutors.FirstOrDefaultAsync(x => x.Id == id);
            _db.Attach(tutor);
            tutor.PhotoPath = photoName;

            await _db.SaveChangesAsync();
        }
    }
}