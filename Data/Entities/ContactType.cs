using System.Collections.Generic;

namespace Data.Entities
{
    public class ContactType
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Contact> Contacts { get; set; }

        public ContactType()
        {
            Contacts = new List<Contact>();
        }
    }
}