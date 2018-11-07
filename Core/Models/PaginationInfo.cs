using System;

namespace Core.Models
{
    public class PaginationInfo
    {
        public int ItemsCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string CurrentSubject { get; set; }

        public int TotalPages => ItemsCount >= PageSize ? (int)Math.Ceiling((decimal)ItemsCount / PageSize) : 1;
        public bool HasPrevPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int PrevPageNumber => HasPrevPage ? PageNumber - 1 : 1;
        public int NextPageNumber => HasNextPage ? PageNumber + 1 : TotalPages;

        public string PrevPageSymbol => "&laquo;";
        public string NextPageSymbol => "&raquo;";
        public string PrevPageText => "Previous";
        public string NextPageText => "Next";
    }
}