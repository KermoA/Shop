using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.ServiceInterface;
using Shop.Data;


namespace Shop.ApplicationServices.Services
{
	public class SpaceshipsServices : ISpaceshipsServices
	{
		private readonly ShopContext _context;

		public SpaceshipsServices
			(
			ShopContext context
			)
		{
			_context = context;
		}

		public async Task<Spaceship> DetailAsync(Guid id)
		{
			var result = await _context.Spaceships
				.FirstOrDefaultAsync( x => x.Id == id );

			return result;
		}
	}
}
