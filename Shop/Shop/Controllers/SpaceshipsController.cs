using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
	public class SpaceshipsController : Controller
	{
		private readonly ShopContext _context;

		public SpaceshipsController
			(
			ShopContext context
			)
		{
			_context = context;
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
	}
}
