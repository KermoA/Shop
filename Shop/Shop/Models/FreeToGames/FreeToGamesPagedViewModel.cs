namespace Shop.Models.FreeToGames
{
    public class FreeToGamesPagedViewModel
    {
        public IEnumerable<FreeToGamesIndexViewModel> FreeGames { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<string> SelectedGenres { get; set; } = new List<string>();
        public List<string> SelectedPlatforms { get; set; } = new List<string>();
        public List<string> AvailableGenres { get; set; } = new List<string>
        {
            "MMORPG", "Shooter", "MOBA", "Anime", "Battle Royale", "Strategy",
            "Fantasy", "Sci-Fi", "Card Games", "Racing", "Fighting", "Social", "Sports"
        };
        public List<string> AvailablePlatforms { get; set; } = new List<string>
        {
            "PC (Windows)", "Web Browser"
        };
    }
}
