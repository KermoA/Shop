namespace Shop.Models.FreeToGames
{
    public class FreeToGamesPagedViewModel
    {
        public IEnumerable<FreeToGamesIndexViewModel> FreeGames { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
