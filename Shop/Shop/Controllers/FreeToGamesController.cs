using Microsoft.AspNetCore.Mvc;
using Shop.Core.ServiceInterface;
using Shop.Core.Dto.FreeToGameDtos;
using System.Linq;
using System.Threading.Tasks;
using Shop.Models.FreeToGames;
using System;
using System.Collections.Generic;

namespace Shop.Controllers
{
    public class FreeToGamesController : Controller
    {
        private readonly IFreeToGamesServices _freeToGamesServices;

        public FreeToGamesController(IFreeToGamesServices freeToGamesServices)
        {
            _freeToGamesServices = freeToGamesServices;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 15, List<string> genres = null, List<string> platforms = null)
        {
            ViewData["Title"] = "Free Games";

            // Get the list of games
            var gamesList = await _freeToGamesServices.FreeToGameResult();

            // Filter by genres if provided
            if (genres != null && genres.Any())
            {
                gamesList = gamesList.Where(game => genres.Contains(game.Genre)).ToList();
            }

            // Filter by platforms if provided
            if (platforms != null && platforms.Any())
            {
                gamesList = gamesList.Where(game => platforms.Contains(game.Platform)).ToList();
            }

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
                PageSize = pageSize,
                SelectedGenres = genres ?? new List<string>(),
                SelectedPlatforms = platforms ?? new List<string>()
            };

            return View(pagedViewModel);
        }
    }
}
