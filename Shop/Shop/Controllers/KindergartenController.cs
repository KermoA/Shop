using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Kindergartens;

namespace KindergartenProject.Controllers
{
    public class KindergartensController : Controller
    {
        private readonly ShopContext _context;

        public KindergartensController
            (
                ShopContext context
            )
        {
            _context = context;
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
    }
}