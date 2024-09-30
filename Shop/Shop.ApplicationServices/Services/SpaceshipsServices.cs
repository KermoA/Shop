using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
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

		public async Task<Spaceship> Create(SpaceshipDto dto)
		{
			Spaceship spaceship = new Spaceship();

			spaceship.Id = Guid.NewGuid();
			spaceship.Name = dto.Name;
			spaceship.Typename = dto.Typename;
			spaceship.SpaceshipModel = dto.SpaceshipModel;
			spaceship.BuiltDate = dto.BuiltDate;
			spaceship.Crew = dto.Crew;
			spaceship.EnginePower = dto.EnginePower;
			spaceship.CreatedAt = DateTime.Now;
			spaceship.ModifiedAt = DateTime.Now;

			await _context.Spaceships.AddAsync( spaceship );
			await _context.SaveChangesAsync();

			return spaceship;

		}

		public async Task<Spaceship> DetailAsync(Guid id)
		{
			var result = await _context.Spaceships
				.FirstOrDefaultAsync( x => x.Id == id );

			return result;
		}

		public async Task<Spaceship> Update(SpaceshipDto dto)
		{
			Spaceship domain = new();

			domain.Id = dto.Id;
			domain.Name = dto.Name;
			domain.Typename = dto.Typename;
			domain.SpaceshipModel = dto.SpaceshipModel;
			domain.BuiltDate = dto.BuiltDate;
			domain.Crew = dto.Crew;
			domain.EnginePower = dto.EnginePower;
			domain.CreatedAt = dto.CreatedAt;
			domain.ModifiedAt = DateTime.Now;

			_context.Spaceships.Update(domain);
			await _context.SaveChangesAsync();

			return domain;
		}

        public async Task<Spaceship> Delete(Guid id)
        {
            var spaceship = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Spaceships.Remove(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }
    }
}