using Microsoft.AspNetCore.Mvc;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Kindergartens;

namespace KindergartenProject.Controllers
{
	public class KindergartensController : Controller
	{
		private readonly ShopContext _context;
		private readonly IKindergartensServices _kindergartenServices;

		public KindergartensController
			(
				ShopContext context,
				IKindergartensServices kindergartensServices
			)
		{
			_context = context;
			_kindergartenServices = kindergartensServices;
		}
		public IActionResult Index()
		{
			var result = _context.Kindergartens
				.Select(x => new KindergartensIndexViewModel
				{
					Id = x.Id,
					GroupName = x.GroupName,
					ChildrenCount = x.ChildrenCount,
					KindergartenName = x.KindergartenName,
					Teacher = x.Teacher
				});

			return View(result);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var kindergarten = await _kindergartenServices.DetailAsync(id);

			if (kindergarten == null)
			{
				return NotFound();
			}

			var vm = new KindergartenDetailsViewModel();

			vm.Id = kindergarten.Id;
			vm.GroupName = kindergarten.GroupName;
			vm.ChildrenCount = kindergarten.ChildrenCount;
			vm.KindergartenName = kindergarten.KindergartenName;
			vm.Teacher = kindergarten.Teacher;
			vm.CreatedAt = kindergarten.CreatedAt;
			vm.UpdatedAt = kindergarten.UpdatedAt;

			return View(vm);

		}
	}
}