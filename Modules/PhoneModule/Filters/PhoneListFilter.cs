using Core.Models;

namespace Modules.PhoneModule.Filters
{
    public class PhoneListFilter : Filter
    {
        public string Number { get; set; }
        public string Operator { get; set; }

        public long? TutorId { get; set; }
    }
}