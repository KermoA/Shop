using Microsoft.AspNetCore.Mvc;
using Shop.Core.ServiceInterface;
using Shop.Data;
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
                    BuildingType = x.BuildingType,
                });
            return View(result);
        }
    }
}