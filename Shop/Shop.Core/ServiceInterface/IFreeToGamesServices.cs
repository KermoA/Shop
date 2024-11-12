using Shop.Core.Dto.FreeToGameDtos;

namespace Shop.Core.ServiceInterface
{
    public interface IFreeToGamesServices
    {
        Task<List<FreeToGamesRootDto>> FreeToGameResult();
    }
}
