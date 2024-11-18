using Shop.Core.Dto.CocktailDtos;
using static Shop.Core.Dto.CocktailDtos.CocktailRootDto;

namespace Shop.Core.ServiceInterface
{
    public interface ICocktailsServices
    {
        Task<List<Drink>> CocktailResult(string drinkName);
    }
}
