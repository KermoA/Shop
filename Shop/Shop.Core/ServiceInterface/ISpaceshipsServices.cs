using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
	public interface ISpaceshipsServices
	{
		Task<Spaceship> DetailAsync(Guid id);
		Task<Spaceship> Update(SpaceshipDto dto);
		Task<Spaceship> Delete(Guid id);
	}
}
