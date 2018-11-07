using System.Collections.Generic;

namespace Data.Entities
{
    public class Specialization
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<TutorSpecialization> TutorSpecializations { get; set; }

        public Specialization()
        {
            TutorSpecializations = new List<TutorSpecialization>();
        }
    }
}