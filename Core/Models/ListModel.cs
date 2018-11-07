using System.Collections.Generic;

namespace Core.Models
{
    public class ListModel<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}