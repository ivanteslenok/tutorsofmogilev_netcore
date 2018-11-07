using System.Collections.Generic;

namespace Data.Entities
{
    public class Subject
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<TutorSubject> TutorSubjects { get; set; }

        public Subject()
        {
            TutorSubjects = new List<TutorSubject>();
        }
    }
}