using System.Collections.Generic;

namespace Data.DTOs
{
    public class TutorDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PhotoPath { get; set; }
        public string Description { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public string Address { get; set; }
        public byte? Experience { get; set; }
        public decimal? Cost { get; set; }
        public int? Rating { get; set; }
        public bool IsVisible { get; set; }
        
        public DistrictDTO District { get; set; }

        public IList<PhoneDTO> Phones { get; set; }
        public IList<ContactDTO> Contacts { get; set; }

        public IList<SpecializationDTO> Specializations { get; set; }
        public IList<SubjectDTO> Subjects { get; set; }
    }
}
