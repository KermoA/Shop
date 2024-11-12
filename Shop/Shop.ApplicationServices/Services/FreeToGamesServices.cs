using Shop.Core.Dto.FreeToGameDtos;
using Shop.Core.ServiceInterface;
using System.Net;
using System.Text.Json;

namespace Shop.ApplicationServices.Services
{
    public class FreeToGamesServices : IFreeToGamesServices
    {
        public async Task<List<FreeToGamesRootDto>> FreeToGameResult()
        {
            var url = "https://www.freetogame.com/api/games";
            List<FreeToGamesRootDto> gamesList = new List<FreeToGamesRootDto>();

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                var freeGameResults = JsonSerializer.Deserialize<List<FreeToGamesRootDto>>(json);

                if (freeGameResults != null)
                {
                    gamesList.AddRange(freeGameResults);
                }
            }

            return gamesList;
        }
    }
}
