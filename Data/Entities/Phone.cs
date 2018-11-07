namespace Data.Entities
{
    public class Phone
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Operator { get; set; }

        public long TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}