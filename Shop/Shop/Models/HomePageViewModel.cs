using Shop.Core.Domain;
using Shop.Core.Dto.ChuckNorrisJokesDtos;

namespace Shop.Models
{
	public class HomePageViewModel
	{
		public IEnumerable<Spaceship> Spaceships { get; set; }
		public IEnumerable<Kindergarten> Kindergartens { get; set; }
		public IEnumerable<RealEstate> RealEstates { get; set; }
        public IEnumerable<FileToApi> FileToApis { get; set; }
        public IEnumerable<KindergartenFileToDatabase> KindergartenImages { get; set; }
        public IEnumerable<FileToDatabase> RealEstateImages { get; set; }
    }
}
