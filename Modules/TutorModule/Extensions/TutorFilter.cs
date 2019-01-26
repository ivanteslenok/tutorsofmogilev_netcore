using System.Linq;
using Modules.TutorModule.Filters;
using Data.Entities;

namespace Modules.TutorModule.Extensions
{
    public static class TutorFilter
    {
        public static IQueryable<Tutor> ApplyFiltering(this IQueryable<Tutor> query, TutorListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                query = query.Where(x => x.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.LastName)
                    && x.LastName.Contains(filter.LastName)
                );

            if (!string.IsNullOrWhiteSpace(filter.Patronymic))
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Patronymic)
                    && x.Patronymic.Contains(filter.Patronymic)
                );

            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Description)
                    && x.Description.Contains(filter.Description)
                );

            if (!string.IsNullOrWhiteSpace(filter.Education))
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Education)
                    && x.Education.Contains(filter.Education)
                );

            if (!string.IsNullOrWhiteSpace(filter.Job))
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Job)
                    && x.Job.Contains(filter.Job)
                );

            if (!string.IsNullOrWhiteSpace(filter.Address))
                query = query.Where(x => 
                    !string.IsNullOrWhiteSpace(x.Address)
                    && x.Address.Contains(filter.Address)
                );

            if (filter.Rating != null)
                query = query.Where(x => x.Rating == filter.Rating);

            if (filter.IsVisible != null)
                query = query.Where(x => x.IsVisible == filter.IsVisible);

            if (filter.ExperienceMin != null)
                query = query.Where(x => x.Experience >= filter.ExperienceMin);

            if (filter.ExperienceMax != null)
                query = query.Where(x => x.Experience <= filter.ExperienceMax);

            if (filter.CostMin != null)
                query = query.Where(x => x.Cost >= filter.CostMin);

            if (filter.CostMax != null)
                query = query.Where(x => x.Cost <= filter.CostMax);

            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
                query = query.Where(x => 
                    x.Phones.Any(p => 
                        !string.IsNullOrWhiteSpace(p.Number)
                        && p.Number.Contains(filter.PhoneNumber)
                        )
                );

            if (!string.IsNullOrWhiteSpace(filter.ContactValue))
                query = query.Where(x => 
                    x.Contacts.Any(c => 
                        !string.IsNullOrWhiteSpace(c.Value)
                        && c.Value.Contains(filter.ContactValue)
                    )
                );

            if (filter.SubjectId != null)
                query = query.Where(x => 
                    x.TutorSubjects.Any(ts => ts.SubjectId == filter.SubjectId)
                );

            if (filter.CityId != null)
                query = query.Where(x => x.CityId == filter.CityId);

            if (filter.SpecializationId != null)
                query = query.Where(x => 
                    x.TutorSpecializations.Any(ts => ts.SpecializationId == filter.SpecializationId)
                );

            if (filter.ContactTypeId != null)
                query = query.Where(x => 
                    x.Contacts.Any(s => s.ContactTypeId == filter.ContactTypeId)
                );

            if (!string.IsNullOrEmpty(filter.Subject))
                query = query.Where(t => 
                    t.TutorSubjects.Any(ts => 
                        ts.Subject.Name == filter.Subject
                    )
                );

            if (!string.IsNullOrEmpty(filter.City))
                query = query.Where(t => t.City.Name == filter.City);

            return query;
        }
    }
}