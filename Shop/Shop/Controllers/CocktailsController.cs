using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.CocktailDtos;
using Shop.Core.ServiceInterface;
using Shop.Models.Cocktails;

public class CocktailsController : Controller
{
    private readonly ICocktailsServices _cocktailsServices;

    public CocktailsController(ICocktailsServices cocktailsServices)
    {
        _cocktailsServices = cocktailsServices;
    }

    // This method handles the search request and returns cocktails based on the search query
    [HttpGet]
    public async Task<IActionResult> Index(string search)
    {
        // If search is null or empty, return an empty list or all cocktails
        if (string.IsNullOrEmpty(search))
        {
            // You can fetch all cocktails or return an empty list
            return View(new CocktailsIndexViewModel { drinks = new List<CocktailsIndexViewModel.Drink>() });
        }

        // Get cocktails from the API based on the search term
        var cocktails = await _cocktailsServices.CocktailResult(search);

        // Map the result to the ViewModel
        var viewModel = new CocktailsIndexViewModel
        {
            drinks = cocktails.Select(cocktail => new CocktailsIndexViewModel.Drink
            {
                idDrink = cocktail.idDrink,
                strDrink = cocktail.strDrink,
                strDrinkAlternate = cocktail.strDrinkAlternate,
                strTags = cocktail.strTags,
                strVideo = cocktail.strVideo,
                strCategory = cocktail.strCategory,
                strIBA = cocktail.strIBA,
                strAlcoholic = cocktail.strAlcoholic,
                strGlass = cocktail.strGlass,
                strInstructions = cocktail.strInstructions,
                strInstructionsES = cocktail.strInstructionsES,
                strInstructionsDE = cocktail.strInstructionsDE,
                strInstructionsFR = cocktail.strInstructionsFR,
                strInstructionsIT = cocktail.strInstructionsIT,
                strInstructionsZHHANS = cocktail.strInstructionsZHHANS,
                strInstructionsZHHANT = cocktail.strInstructionsZHHANT,
                strDrinkThumb = cocktail.strDrinkThumb,
                strIngredient1 = cocktail.strIngredient1,
                strIngredient2 = cocktail.strIngredient2,
                strIngredient3 = cocktail.strIngredient3,
                strIngredient4 = cocktail.strIngredient4,
                strIngredient5 = cocktail.strIngredient5,
                strIngredient6 = cocktail.strIngredient6,
                strIngredient7 = cocktail.strIngredient7,
                strIngredient8 = cocktail.strIngredient8,
                strIngredient9 = cocktail.strIngredient9,
                strIngredient10 = cocktail.strIngredient10,
                strIngredient11 = cocktail.strIngredient11,
                strIngredient12 = cocktail.strIngredient12,
                strIngredient13 = cocktail.strIngredient13,
                strIngredient14 = cocktail.strIngredient14,
                strIngredient15 = cocktail.strIngredient15,
                strMeasure1 = cocktail.strMeasure1,
                strMeasure2 = cocktail.strMeasure2,
                strMeasure3 = cocktail.strMeasure3,
                strMeasure4 = cocktail.strMeasure4,
                strMeasure5 = cocktail.strMeasure5,
                strMeasure6 = cocktail.strMeasure6,
                strMeasure7 = cocktail.strMeasure7,
                strMeasure8 = cocktail.strMeasure8,
                strMeasure9 = cocktail.strMeasure9,
                strMeasure10 = cocktail.strMeasure10,
                strMeasure11 = cocktail.strMeasure11,
                strMeasure12 = cocktail.strMeasure12,
                strMeasure13 = cocktail.strMeasure13,
                strMeasure14 = cocktail.strMeasure14,
                strMeasure15 = cocktail.strMeasure15,
                strImageSource = cocktail.strImageSource,
                strImageAttribution = cocktail.strImageAttribution,
                strCreativeCommonsConfirmed = cocktail.strCreativeCommonsConfirmed,
                dateModified = cocktail.dateModified
            }).ToList()
        };

        return View(viewModel);
    }
}
