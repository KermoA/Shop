using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationServices.Services;
using Shop.Core.ServiceInterface;
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

		public HomeController(
            ILogger<HomeController> logger,
			ISpaceshipsServices spaceshipService,
			IKindergartensServices kindergartenService,
			IRealEstateServices realEstateService,
            IFileServices fileService
            )
        {
            _logger = logger;
			_spaceshipService = spaceshipService;
			_kindergartenService = kindergartenService;
			_realEstateService = realEstateService;
            _fileService = fileService;
        }

		public IActionResult Index()
		{
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

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
