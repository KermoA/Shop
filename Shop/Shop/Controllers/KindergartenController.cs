using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
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
		public IActionResult Create()
		{
			KindergartenCreateUpdateViewModel result = new();

			return View("CreateUpdate", result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
		{
			var dto = new KindergartenDto()
			{
				Id = vm.Id,
				GroupName = vm.GroupName,
				ChildrenCount = vm.ChildrenCount,
				KindergartenName = vm.KindergartenName,
				Teacher = vm.Teacher,
				CreatedAt = vm.CreatedAt,
				UpdatedAt = vm.UpdatedAt,
			};

			var result = await _kindergartenServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
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

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var kindergarten = await _kindergartenServices.DetailAsync(id);

			if (kindergarten == null)
			{
				return NotFound();
			}

			var vm = new KindergartenCreateUpdateViewModel();

			vm.Id = kindergarten.Id;
			vm.GroupName = kindergarten.GroupName;
			vm.ChildrenCount = kindergarten.ChildrenCount;
			vm.KindergartenName = kindergarten.KindergartenName;
			vm.Teacher = kindergarten.Teacher;
			vm.CreatedAt = kindergarten.CreatedAt;
			vm.UpdatedAt = kindergarten.UpdatedAt;

			return View("CreateUpdate", vm);
		}

		[HttpPost]
		public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
		{
			var dto = new KindergartenDto()
			{
				Id = vm.Id,
				GroupName = vm.GroupName,
				ChildrenCount = vm.ChildrenCount,
				KindergartenName = vm.KindergartenName,
				Teacher = vm.Teacher,
				CreatedAt = vm.CreatedAt,
				UpdatedAt = vm.UpdatedAt,
			};

			var result = await _kindergartenServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var kindergarten = await _kindergartenServices.DetailAsync(id);

			if (kindergarten == null)
			{
				return NotFound();
			}

			var vm = new KindergartenDeleteViewModel();

			vm.Id = id;
			vm.GroupName = kindergarten.GroupName;
			vm.ChildrenCount = kindergarten.ChildrenCount;
			vm.KindergartenName = kindergarten.KindergartenName;
			vm.Teacher = kindergarten.Teacher;
			vm.CreatedAt = kindergarten.CreatedAt;
			vm.UpdatedAt = kindergarten.UpdatedAt;

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var kindergarten = await _kindergartenServices.Delete(id);

			if (kindergarten == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
	}
}