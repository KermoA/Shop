using Shop.Core.Dto.CocktailDtos;
using Shop.Core.ServiceInterface;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using static Shop.Core.Dto.CocktailDtos.CocktailRootDto;

namespace Shop.ApplicationServices.Services
{
    public class CocktailsServices : ICocktailsServices
    {
        public async Task<List<Drink>> CocktailResult(string drinkName)
        {
            if (string.IsNullOrEmpty(drinkName))
                throw new ArgumentException("Drink name cannot be null or empty", nameof(drinkName));

            var url = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={WebUtility.UrlEncode(drinkName)}";
            var cocktailList = new List<Drink>();

            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = await client.DownloadStringTaskAsync(url);

                    var root = JsonSerializer.Deserialize<CocktailRootDto>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (root?.Drinks != null)
                    {
                        cocktailList.AddRange(root.Drinks);
                    }
                }
                catch (WebException webEx)
                {
                    throw new Exception("An error occurred while fetching cocktails from the API", webEx);
                }
                catch (JsonException jsonEx)
                {
                    throw new Exception("An error occurred while parsing the cocktail data", jsonEx);
                }
            }

            return cocktailList;
        }
    }
}
