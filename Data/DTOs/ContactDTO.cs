namespace Data.DTOs
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public ContactTypeDTO ContactType { get; set; }
    }
}
