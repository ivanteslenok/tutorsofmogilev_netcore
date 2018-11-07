using System.Collections.Generic;

namespace Data.Entities
{
    public class District
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tutor> Tutors { get; set; }

        public District()
        {
            Tutors = new List<Tutor>();
        }
    }
}