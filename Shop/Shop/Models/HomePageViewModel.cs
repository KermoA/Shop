using Shop.Core.Domain;
using Shop.Models.Kindergartens;
using Shop.Models.RealEstates;
using Shop.Models.Spaceships;

namespace Shop.Models
{
	public class HomePageViewModel
	{
		public IEnumerable<Spaceship> Spaceships { get; set; }
		public IEnumerable<Kindergarten> Kindergartens { get; set; }
		public IEnumerable<RealEstate> RealEstates { get; set; }
    }
}
