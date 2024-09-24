using Shop.Core.Domain;

namespace Shop.Core.ServiceInterface
{
	public interface ISpaceshipsServices
	{
		Task<Spaceship> DetailAsync(Guid id);
	}
}
