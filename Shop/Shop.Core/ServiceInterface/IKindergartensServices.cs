using Shop.Core.Domain;

namespace Shop.Core.ServiceInterface
{
	public interface IKindergartensServices
	{
		Task<Kindergarten> DetailAsync(Guid id);
	}
}
