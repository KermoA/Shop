namespace Shop.Models.Spaceships
{
    public class SpaceshipsPagedViewModel
    {
        public IEnumerable<SpaceshipsIndexViewModel> Spaceships { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
