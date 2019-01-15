using System.Collections.Generic;

namespace Data.Entities
{
    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tutor> Tutors { get; set; }

        public City()
        {
            Tutors = new List<Tutor>();
        }
    }
}
