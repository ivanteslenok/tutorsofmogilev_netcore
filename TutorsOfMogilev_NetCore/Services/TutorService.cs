using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Modules.CityModule;
using Modules.SubjectModule;
using Modules.SubjectModule.Filters;
using Modules.TutorModule;
using Modules.TutorModule.Filters;
using TutorsOfMogilev_NetCore.Models;

namespace TutorsOfMogilev_NetCore.Services
{
    public class TutorService
    {
        private readonly SubjectRepository _subjectRepo;
        private readonly CityRepository _cityRepo;
        private readonly TutorRepository _tutorRepo;
        private readonly IMapper _mapper;
        IConfiguration _config;

        public TutorService(
            SubjectRepository subjectRepo,
            CityRepository cityRepo,
            TutorRepository tutorRepo,
            IMapper mapper,
            IConfiguration config
            )
        {
            _subjectRepo = subjectRepo;
            _cityRepo = cityRepo;
            _tutorRepo = tutorRepo;
            _mapper = mapper;
            _config = config;
        }

        public async Task<TutorsListVM> GetListVM(TutorListFilter filter)
        {
            var totorsList = await _tutorRepo.GetList(filter);
            var subjects = await _subjectRepo.GetList(new SubjectListFilter { City = filter.City });

            return new TutorsListVM
            {
                Tutors = totorsList.Items.Select(x => new TutorVM()
                {
                    Title = $"{x.FirstName} {x.LastName}" + (!string.IsNullOrWhiteSpace(x.Patronymic) ? $" {x.Patronymic}" : ""),
                    PhotoPath = x.PhotoPath,
                    Cost = x.Cost.ToString(),
                    City = x.City.Name,
                    Education = x.Education,
                    Description = x.Description,
                    Specializations = x.Specializations
                        .Aggregate(
                            new StringBuilder(),
                            (acc, next) => StringAggregator(acc, next.Name)
                            )
                        .ToString(),
                    Subjects = x.Subjects
                        .Aggregate(
                            new StringBuilder(),
                            (acc, next) => StringAggregator(acc, next.Name)
                            )
                        .ToString(),
                    UrlKey = $"{x.Id}-{x.CreateDate.ToString("ddMMyyyy")}"
                }).ToList(),
                Subjects = subjects.Items,
                Cities = await _cityRepo.GetList(),
                PaginationInfo = new PaginationInfo
                {
                    ItemsCount = totorsList.TotalCount,
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    CurrentSubject = filter.Subject,
                    CurrentCity = filter.City
                }
            };
        }

        public async Task<TutorAdvancedVM> GetItemVM(long id)
        {
            var tutor = await _tutorRepo.GetItem(id);
            var vm = _mapper.Map<TutorAdvancedVM>(tutor);
            var rand = new Random();
            var phoneNumb = new Random().Next(0, 3);
            vm.Phone = _config.GetSection($"Phones:{phoneNumb}").Value;

            return vm;
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