using Core.Models;

namespace Modules.TutorModule.Filters
{
    public class TutorListFilter : Filter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Description { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public int? Rating { get; set; }
        public bool? IsVisible { get; set; }
        
        public byte? ExperienceMin { get; set; }
        public byte? ExperienceMax { get; set; }
        public decimal? CostMin { get; set; }
        public decimal? CostMax { get; set; }

        public string PhoneNumber { get; set; }
        public string ContactValue { get; set; }

        public long? SubjectId { get; set; }
        public long? DistrictId { get; set; }
        public long? SpecializationId { get; set; }
        public long? ContactTypeId { get; set; }

        public string Subject { get; set; }
    }
}