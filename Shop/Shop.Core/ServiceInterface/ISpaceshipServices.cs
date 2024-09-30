using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
	public interface ISpaceshipServices
	{
		Task<Spaceship> DetailAsync(Guid id);
		Task<Spaceship> Update(SpaceshipDto dto);
		Task<Spaceship> Delete(Guid id);
		Task<Spaceship> Create(SpaceshipDto dto);
	}
}
