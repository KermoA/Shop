using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.ApplicationServices.Services;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Kindergartens;
using Shop.Models.RealEstates;

namespace Shop.Controllers
{
	public class RealEstatesController : Controller
	{
		public readonly ShopContext _context;
		private readonly IRealEstateServices _realEstateServices;
		private readonly IFileServices _fileServices;

		public RealEstatesController
			(
			ShopContext context,
			IRealEstateServices realEstateServices,
			IFileServices fileServices
			)
		{
			_context = context;
			_realEstateServices = realEstateServices;
			_fileServices = fileServices;
		}

        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string sortOrder = null)
        {
            ViewData["Title"] = "Real Estates";
            var query = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Size = x.Size,
                    Location = x.Location,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType,
					CreatedAt = x.CreatedAt,
                });

            switch (sortOrder)
            {
				case "location":
					query = query.OrderBy(r => r.Location);
					break;
                case "location_desc":
                    query = query.OrderByDescending(r => r.Location);
                    break;
                case "size":
                    query = query.OrderBy(r => r.Size);
                    break;
                case "size_desc":
                    query = query.OrderByDescending(r => r.Size);
                    break;
                case "room":
                    query = query.OrderBy(r => r.RoomNumber);
                    break;
                case "room_desc":
                    query = query.OrderByDescending(r => r.RoomNumber);
                    break;
                case "type":
                    query = query.OrderBy(r => r.BuildingType);
                    break;
                case "type_desc":
                    query = query.OrderByDescending(r => r.BuildingType);
                    break;
				case "created_at":
					query = query.OrderBy(r => r.CreatedAt);
					break;
                default:
                    query = query.OrderByDescending(r => r.CreatedAt);
                    break;
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var realEstates = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new RealEstatesPagedViewModel
            {
                RealEstates = realEstates,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize,
                SortOrder = sortOrder
            };

            return View(model);
        }

        [HttpGet]
		public IActionResult Create()
		{
            ViewData["Title"] = "Create Real Estate";
            RealEstateCreateUpdateViewModel realEstates = new();

			return View("CreateUpdate", realEstates);
		}

		[HttpPost]
		public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
		{
			var dto = new RealEstateDto()
			{
				Id = vm.Id,
				Size = vm.Size,
				Location = vm.Location,
				RoomNumber = vm.RoomNumber,
				BuildingType = vm.BuildingType,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt,
				Files = vm.Files,
				Image = vm.Image
					.Select(x => new FileToDatabaseDto
					{
						Id = x.ImageId,
						ImageData = x.ImageData,
						ImageTitle = x.ImageTitle,
						realEstateId = x.RealEstateId
					}).ToArray()
			};

			var result = await _realEstateServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
            ViewData["Title"] = "Real Estate Details";
            var realEstates = await _realEstateServices.GetAsync(id);

			if (realEstates == null)
			{
				return View("Error");
			}

			var photos = await _context.FileToDatabases
				.Where(x => x.RealEstateId == id)
				.Select(y => new RealEstateImageViewModel
				{
					RealEstateId = y.Id,
					ImageId = y.Id,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new RealEstatesDetailsViewModel();

			vm.Id = realEstates.Id;
			vm.Size = realEstates.Size;
			vm.Location = realEstates.Location;
			vm.RoomNumber = realEstates.RoomNumber;
			vm.BuildingType = realEstates.BuildingType;
			vm.CreatedAt = realEstates.CreatedAt;
			vm.ModifiedAt = realEstates.ModifiedAt;
			vm.Image.AddRange(photos);

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
            ViewData["Title"] = "Update Real Estate";
            var realEstates = await _realEstateServices.GetAsync(id);
			if (realEstates == null)
			{
				return NotFound();
			}

			var photos = await _context.FileToDatabases
				.Where(x => x.RealEstateId == id)
				.Select(y => new RealEstateImageViewModel
				{
					RealEstateId = y.Id,
					ImageId = y.Id,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

			var vm = new RealEstateCreateUpdateViewModel();

			vm.Id = realEstates.Id;
			vm.Size = realEstates.Size;
			vm.Location = realEstates.Location;
			vm.RoomNumber = realEstates.RoomNumber;
			vm.BuildingType = realEstates.BuildingType;
			vm.CreatedAt = realEstates.CreatedAt;
			vm.ModifiedAt = realEstates.ModifiedAt;
			vm.Image.AddRange(photos);

			return View("CreateUpdate", vm);
		}


		[HttpPost]
		public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
		{
			var dto = new RealEstateDto()
			{
				Id = vm.Id,
				Size = vm.Size,
				Location = vm.Location,
				RoomNumber = vm.RoomNumber,
				BuildingType = vm.BuildingType,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt,
				Files = vm.Files,
				Image = vm.Image
					.Select(x => new FileToDatabaseDto
					{
						Id = x.ImageId,
						ImageData = x.ImageData,
						ImageTitle = x.ImageTitle,
						realEstateId = x.RealEstateId,
					}).ToArray()
			};
			var result = await _realEstateServices.Update(dto);
			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Index), vm);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
            ViewData["Title"] = "Delete Real Estate";
            var realEstates = await _realEstateServices.GetAsync(id);

			if (realEstates == null)
			{
				return NotFound();
			}

			var photos = await _context.FileToDatabases
			   .Where(x => x.RealEstateId == id)
			   .Select(y => new RealEstateImageViewModel
			   {
				   RealEstateId = y.Id,
				   ImageId = y.Id,
				   ImageData = y.ImageData,
				   ImageTitle = y.ImageTitle,
				   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
			   }).ToArrayAsync();

			var vm = new RealEstateDeleteViewModel();
			vm.Id = realEstates.Id;
			vm.Size = realEstates.Size;
			vm.Location = realEstates.Location;
			vm.RoomNumber = realEstates.RoomNumber;
			vm.BuildingType = realEstates.BuildingType;
			vm.CreatedAt = realEstates.CreatedAt;
			vm.ModifiedAt = realEstates.ModifiedAt;
			vm.Image.AddRange(photos);

			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var realEstates = await _realEstateServices.Delete(id);
			if (realEstates == null)
			{
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Index));
		}

        [HttpPost]
        public async Task<IActionResult> RemoveImage([FromBody] RealEstateImageViewModel vm)
        {
            if (vm.ImageId == Guid.Empty)
            {
                return BadRequest("Invalid Image ID.");
            }

            var dto = new FileToDatabaseDto { Id = vm.ImageId };
            var result = await _fileServices.RemoveImageFromDatabase(dto);

            if (result != null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}