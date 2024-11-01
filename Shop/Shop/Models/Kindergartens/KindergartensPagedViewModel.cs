namespace Shop.Models.Kindergartens
{
    public class KindergartensPagedViewModel
    {
        public IEnumerable<KindergartensIndexViewModel> Kindergartens { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SortOrder { get; set; }
    }
}
