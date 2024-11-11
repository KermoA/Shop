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
            return View();
        }

        [HttpPost]
        public IActionResult NewJoke()
        {
            return RedirectToAction("ChuckNorrisJoke");
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

            var vm = new ChuckNorrisJokesViewModel
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
