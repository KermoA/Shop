using Shop.Core.ServiceInterface;
using Shop.Data;
namespace Shop.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopContext _context;

        public RealEstateServices
            (
                ShopContext context
            )
        {
            _context = context;
        }
    }
}