using Core.Models;

namespace Modules.SubjectModule.Filters
{
    public class SubjectListFilter : Filter
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}