namespace Data.Entities
{
    public class TutorSpecialization
    {
        public long TutorId { get; set; }
        public Tutor Tutor { get; set; }

        public long SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}