namespace Shop.Models.RealEstates
{
    public class RealEstatesPagedViewModel
    {
        public IEnumerable<RealEstateIndexViewModel> RealEstates { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortOrder { get; set; }
    }
}
