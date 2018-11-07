using Core.Models;

namespace Modules.ContactModule.Filters
{
    public class ContactListFilter : Filter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public long? ContactTypeId { get; set; }
        public long? TutorId { get; set; }
    }
}