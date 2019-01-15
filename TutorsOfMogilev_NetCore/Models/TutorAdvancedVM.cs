using System;

namespace TutorsOfMogilev_NetCore.Models
{
    public class TutorAdvancedVM
    {
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
        public DateTime CreateDate { get; set; }
        public string City { get; set; }
        public string[] Specializations { get; set; }
        public string[] Subjects { get; set; }

        public string Phone { get; set; }
    }
}
