using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Shop.ApplicationServices.Services;
using Shop.Core.ServiceInterface;
using Shop.Hubs;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly ISpaceshipsServices _spaceshipService;
		private readonly IKindergartensServices _kindergartenService;
		private readonly IRealEstateServices _realEstateService;
        private readonly IFileServices _fileService;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;

		public HomeController(
            ILogger<HomeController> logger,
			ISpaceshipsServices spaceshipService,
			IKindergartensServices kindergartenService,
			IRealEstateServices realEstateService,
            IFileServices fileService,
            IHubContext<DeathlyHallowsHub> deathlyHub
            )
        {
            _logger = logger;
			_spaceshipService = spaceshipService;
			_kindergartenService = kindergartenService;
			_realEstateService = realEstateService;
            _fileService = fileService;
            _deathlyHub = deathlyHub;
        }

		public IActionResult Index()
		{
            ViewData["Title"] = "Shop";
            var viewModel = new HomePageViewModel
			{
				Spaceships = _spaceshipService.GetAll(),
				Kindergartens = _kindergartenService.GetAll(),
				RealEstates = _realEstateService.GetAll(),
                FileToApis = _fileService.GetFileToApis(),
                KindergartenImages = _fileService.GetKindergartenFiles(),
                RealEstateImages = _fileService.GetRealEstateFiles()
            };

			return View(viewModel);
		}

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DealthyHallowRace.ContainsKey(type))
            {
                SD.DealthyHallowRace[type]++;
            }
            await _deathlyHub.Clients.All.SendAsync("updateDealthyHallowCount",
                SD.DealthyHallowRace[SD.Cloak],
                SD.DealthyHallowRace[SD.Stone],
                SD.DealthyHallowRace[SD.Wand]);

            return Accepted();
        }

        public IActionResult Chat()
        {
            return View(); 
        }

		public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy Policy";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
