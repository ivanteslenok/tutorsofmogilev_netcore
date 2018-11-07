namespace Core.Models
{
    public class Filter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        public string SortBy { get; set; }
        public bool DescSort { get; set; }
    }
}
