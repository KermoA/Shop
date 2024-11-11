using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.ChuckNorrisJokesDtos;
using Shop.Core.ServiceInterface;
using Shop.Models.ChuckNorrisJokes;

namespace Shop.Controllers
{
    public class ChuckNorrisJokesController : Controller
    {
        private readonly IChuckNorrisJokesServices _chuckNorrisJokesServices;

        public ChuckNorrisJokesController(IChuckNorrisJokesServices chuckNorrisJokesServices)
        {
            _chuckNorrisJokesServices = chuckNorrisJokesServices;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Chuck Norris Jokes";

            if (TempData["IsFirstVisit"] == null)
            {
                TempData["IsFirstVisit"] = true;
                TempData["Joke"] = "Click 'Get Fact' for a Chuck Norris fact!";
            }

            return View();
        }

        [HttpPost]
        public IActionResult NewJoke()
        {
            TempData["IsFirstVisit"] = false;

            ChuckNorrisJokesRootDto dto = new ChuckNorrisJokesRootDto();

            _chuckNorrisJokesServices.ChuckNorrisJokesResult(dto);

            if (!string.IsNullOrEmpty(dto.Value))
            {
                TempData["Joke"] = dto.Value;
            }
            else
            {
                TempData["Joke"] = "Joke not found.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChuckNorrisJoke()
        {
            ChuckNorrisJokesRootDto dto = new ChuckNorrisJokesRootDto();

            _chuckNorrisJokesServices.ChuckNorrisJokesResult(dto);

            if (string.IsNullOrEmpty(dto.Value))
            {
                ModelState.AddModelError(string.Empty, "Joke not found.");
                return View("Error");
            }

            var vm = new ChuckNorrisIndexViewModel
            {
                CreatedAt = dto.CreatedAt,
                IconUrl = dto.IconUrl,
                Id = dto.Id,
                UpdatedAt = dto.UpdatedAt,
                Url = dto.Url,
                Value = dto.Value
            };

            return View(vm);
        }
    }
}
