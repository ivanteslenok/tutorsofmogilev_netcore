using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Tutor
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
        [Column(TypeName = "decimal(7,2)")]
        public decimal? Cost { get; set; }
        public int? Rating { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreateDate { get; set; }

        public long? CityId { get; set; }
        public City City { get; set; }

        // one-to-many

        public ICollection<Phone> Phones { get; set; }
        public ICollection<Contact> Contacts { get; set; }

        // many-to-many

        public ICollection<TutorSpecialization> TutorSpecializations { get; set; }
        public ICollection<TutorSubject> TutorSubjects { get; set; }

        public Tutor()
        {
            Phones = new List<Phone>();
            Contacts = new List<Contact>();
            TutorSpecializations = new List<TutorSpecialization>();
            TutorSubjects = new List<TutorSubject>();
        }
    }
}