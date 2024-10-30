using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<RealEstate> GetAsync(Guid id);
        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate> Update(RealEstateDto dto);
        Task<RealEstate> Delete(Guid id);
        IEnumerable<RealEstate> GetAll();
    }
}