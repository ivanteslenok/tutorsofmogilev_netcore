using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Modules.SubjectModule;
using Modules.TutorModule;
using Modules.TutorModule.Filters;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Services
{
    public class TutorService
    {
        private readonly SubjectRepository _subjectRepo;
        private readonly TutorRepository _tutorRepo;

        public TutorService(SubjectRepository subjectRepo, TutorRepository tutorRepo)
        {
            _subjectRepo = subjectRepo;
            _tutorRepo = tutorRepo;
        }

        public async Task<TutorsListVM> GetListVM(TutorListFilter filter)
        {
            var totorsList = await _tutorRepo.GetList(filter);

            return new TutorsListVM
            {
                Tutors = totorsList.Items.Select(x => new TutorVM()
                {
                    FIO = $"{x.FirstName} {x.LastName}" + (!string.IsNullOrWhiteSpace(x.Patronymic) ? $" {x.Patronymic}" : ""),
                    PhotoPath = x.PhotoPath,
                    Cost = x.Cost.ToString(),
                    District = x.District.Name,
                    Education = x.Education,
                    Specializations = x.TutorSpecializations
                        .Select(ts => ts.Specialization)
                        .Aggregate(
                            new StringBuilder(), 
                            (acc, next) => StringAggregator(acc, next.Name)
                            )
                        .ToString(),
                    Subjects = x.TutorSubjects
                        .Select(ts => ts.Subject)
                        .Aggregate(
                            new StringBuilder(), 
                            (acc, next) => StringAggregator(acc, next.Name)
                            )
                        .ToString()
                }).ToList(),
                Subjects = await _subjectRepo.GetList(),
                PaginationInfo = new PaginationInfo
                {
                    ItemsCount = totorsList.TotalCount,
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    CurrentSubject = filter.Subject
                }
            };
        }

        private StringBuilder StringAggregator(StringBuilder acc, string next)
        {
            if (acc.Length > 0)
                acc.Append((", "));
            acc.Append(next);
            return acc;
        }
    }
}