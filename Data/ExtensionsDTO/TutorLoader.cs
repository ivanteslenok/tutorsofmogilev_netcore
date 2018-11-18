
using System.Linq;
using Data.DTOs;
using Data.Entities;

namespace Data.ExtensionsDTO
{
    public static class TutorLoader
    {
        public static TutorDTO CreateDto(this Tutor entity)
        {
            return new TutorDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Patronymic = entity.Patronymic,
                PhotoPath = entity.PhotoPath,
                Description = entity.Description,
                Education = entity.Education,
                Job = entity.Job,
                Address = entity.Address,
                Experience = entity.Experience,
                Cost = entity.Cost,
                Rating = entity.Rating,
                IsVisible = entity.IsVisible,

                District = entity.District.CreateDto(),

                Phones = entity.Phones.Select(x => x?.CreateDto()).ToList(),
                Contacts = entity.Contacts.Select(x => x?.CreateDto()).ToList(),

                Specializations = entity.TutorSpecializations.Select(x => x?.Specialization.CreateDto()).ToList(),
                Subjects = entity.TutorSubjects.Select(x => x?.Subject.CreateDto()).ToList()
            };
        }
    }
}
