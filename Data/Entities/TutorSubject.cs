namespace Data.Entities
{
    public class TutorSubject
    {
        public long TutorId { get; set; }
        public Tutor Tutor { get; set; }

        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}