using Microsoft.AspNetCore.Mvc;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
	public class SpaceshipsController : Controller
	{
		private readonly ShopContext _context;
		private readonly ISpaceshipsServices _spaceshipServices;

		public SpaceshipsController
			(
			ShopContext context,
			ISpaceshipsServices spaceshipsServices
			)
		{
			_context = context;
			_spaceshipServices = spaceshipsServices;
		}


		public IActionResult Index()
		{
			var result = _context.Spaceships
				.Select(x => new SpaceshipsIndexViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Typename = x.Typename,
					BuiltDate = x.BuiltDate,
					Crew = x.Crew,
				});

			return View(result);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var spaceship = await _spaceshipServices.DetailAsync(id);

			if (spaceship == null) 
			{
				return View("Error");
			}

			var vm = new SpaceshipDetailsViewModel();

			vm.Id = spaceship.Id;
			vm.Name = spaceship.Name;
			vm.Typename = spaceship.Typename;
			vm.BuiltDate = spaceship.BuiltDate;
			vm.SpaceshipModel = spaceship.SpaceshipModel;
			vm.Crew = spaceship.Crew;
			vm.EnginePower = spaceship.EnginePower;
			vm.CreatedAt = spaceship.CreatedAt;
			vm.ModifiedAt = spaceship.ModifiedAt;


			return View(vm);
		}
	}
}
