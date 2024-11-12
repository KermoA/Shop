using Microsoft.AspNetCore.Mvc;
using Shop.Core.ServiceInterface;
using Shop.Core.Dto.FreeToGameDtos;
using System.Threading.Tasks;
using Shop.Models.FreeToGames;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop.Controllers
{
    public class FreeToGamesController : Controller
    {
        private readonly IFreeToGamesServices _freeToGamesServices;

        public FreeToGamesController(IFreeToGamesServices freeToGamesServices)
        {
            _freeToGamesServices = freeToGamesServices;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {
            ViewData["Title"] = "Free Games";

            // Get the list of games
            var gamesList = await _freeToGamesServices.FreeToGameResult();

            // Map the DTOs to ViewModels
            var viewModelList = gamesList.Select(game => new FreeToGamesIndexViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Thumbnail = game.Thumbnail,
                ShortDescription = game.ShortDescription,
                GameUrl = game.GameUrl,
                Genre = game.Genre,
                Platform = game.Platform,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = game.ReleaseDate,
                FreeToGameProfileUrl = game.FreeToGameProfileUrl
            }).ToList();

            // Calculate pagination values
            var totalItems = viewModelList.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedGames = viewModelList
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pagedViewModel = new FreeToGamesPagedViewModel
            {
                FreeGames = pagedGames,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            return View(pagedViewModel);
        }
    }
}
