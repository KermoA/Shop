using Nancy.Json;
using Shop.Core.Dto.ChuckNorrisJokesDtos;
using Shop.Core.ServiceInterface;
using System.Net;

namespace Shop.ApplicationServices.Services
{
	public class ChuckNorrisJokesServices : IChuckNorrisJokesServices
	{
		public async Task<ChuckNorrisJokesRootDto> ChuckNorrisJokesResult(ChuckNorrisJokesRootDto dto)
		{

			string url = $"https://api.chucknorris.io/jokes/random";

			using (WebClient client = new WebClient())
			{
				string json = client.DownloadString(url);
				

				ChuckNorrisJokesRootDto result = new JavaScriptSerializer().Deserialize<ChuckNorrisJokesRootDto>(json);

				dto.Id = result.Id;
				dto.Categories = result.Categories;
				dto.CreatedAt = result.CreatedAt;
				dto.IconUrl = result.IconUrl;
				dto.UpdatedAt = result.UpdatedAt;
				dto.Url = result.Url;
				dto.Value = result.Value;

			}
			return dto;
		}
	}
}

