using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
	public interface IKindergartensServices
	{
		Task<Kindergarten> DetailAsync(Guid id);
		Task<Kindergarten> Update(KindergartenDto dto);
		Task<Kindergarten> Delete(Guid id);
		Task<Kindergarten> Create(KindergartenDto dto);
        IEnumerable<Kindergarten> GetAll();
    }
}
