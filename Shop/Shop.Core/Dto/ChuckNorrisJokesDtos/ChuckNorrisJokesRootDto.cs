using System.Text.Json.Serialization;

namespace Shop.Core.Dto.ChuckNorrisJokesDtos
{
	public class ChuckNorrisJokesRootDto
	{
			[JsonPropertyName("categories")]
			public List<object> Categories { get; set; }

			[JsonPropertyName("created_at")]
			public DateTime CreatedAt { get; set; }

			[JsonPropertyName("icon_url")]
			public string IconUrl { get; set; }

			[JsonPropertyName("id")]
			public string Id { get; set; }

			[JsonPropertyName("updated_at")]
			public DateTime UpdatedAt { get; set; }

			[JsonPropertyName("url")]
			public string Url { get; set; }

			[JsonPropertyName("value")]
			public string Value { get; set; }
	}
}
