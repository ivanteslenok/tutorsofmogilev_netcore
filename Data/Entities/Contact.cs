namespace Data.Entities
{
    public class Contact
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public long? ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }

        public long TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}