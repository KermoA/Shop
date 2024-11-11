using Shop.Core.Dto.ChuckNorrisJokesDtos;

namespace Shop.Core.ServiceInterface
{
	public interface IChuckNorrisJokesServices
	{
		Task<ChuckNorrisJokesRootDto> ChuckNorrisJokesResult(ChuckNorrisJokesRootDto dto);
	}
}
