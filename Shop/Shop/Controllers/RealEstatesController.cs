using Microsoft.AspNetCore.Mvc;
using Shop.ApplicationServices.Services;
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

        public RealEstatesController
            (
            ShopContext context,
            IRealEstateServices realEstateServices
            )
        {
            _context = context;
            _realEstateServices = realEstateServices;
        }

        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Size = x.Size,
                    Location = x.Location,
                    RoomNumber = x.RoomNumber,
                    BuildingType = x.BuildingType

                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
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
                ModifiedAt = vm.ModifiedAt
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
            var realEstates = await _realEstateServices.GetAsync(id);

            if (realEstates == null)
            {
                return View("Error");
            }

            var vm = new RealEstatesDetailsViewModel();

            vm.Id = realEstates.Id;
            vm.Size = realEstates.Size;
            vm.Location = realEstates.Location;
            vm.RoomNumber = realEstates.RoomNumber;
            vm.BuildingType = realEstates.BuildingType;
            vm.CreatedAt = realEstates.CreatedAt;
            vm.ModifiedAt = realEstates.ModifiedAt;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstates = await _realEstateServices.GetAsync(id);
            if (realEstates == null)
            {
                return NotFound();
            }
            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstates.Id;
            vm.Size = realEstates.Size;
            vm.Location = realEstates.Location;
            vm.RoomNumber = realEstates.RoomNumber;
            vm.BuildingType = realEstates.BuildingType;
            vm.CreatedAt = realEstates.CreatedAt;
            vm.ModifiedAt = realEstates.ModifiedAt;

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
                ModifiedAt = vm.ModifiedAt
            };
            var result = await _realEstateServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
    }
}