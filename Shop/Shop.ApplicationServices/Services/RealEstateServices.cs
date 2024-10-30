
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;

namespace Shop.ApplicationServices.Services
{
	public class RealEstateServices : IRealEstateServices
	{
		private readonly ShopContext _context;
		private readonly IFileServices _fileServices;

		public RealEstateServices
			(
				ShopContext context,
				IFileServices fileServices

			)
		{
			_context = context;
			_fileServices = fileServices;
		}

		public async Task<RealEstate> Create(RealEstateDto dto)
		{
			RealEstate realEstate = new();

			realEstate.Id = Guid.NewGuid();
			realEstate.Size = dto.Size;
			realEstate.Location = dto.Location;
			realEstate.RoomNumber = dto.RoomNumber;
			realEstate.BuildingType = dto.BuildingType;
			realEstate.CreatedAt = DateTime.Now;
			realEstate.ModifiedAt = DateTime.Now;

			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabase(dto, realEstate);
			}

			await _context.RealEstates.AddAsync(realEstate);
			await _context.SaveChangesAsync();

			return realEstate;
		}

		public async Task<RealEstate> GetAsync(Guid id)
		{
			var result = await _context.RealEstates
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async Task<RealEstate> Update(RealEstateDto dto)
		{
			RealEstate domain = new();

			domain.Id = dto.Id;
			domain.Size = dto.Size;
			domain.Location = dto.Location;
			domain.RoomNumber = dto.RoomNumber;
			domain.BuildingType = dto.BuildingType;
			domain.CreatedAt = dto.CreatedAt;
			domain.ModifiedAt = DateTime.Now;

			if (dto.Files != null)
			{
				_fileServices.UploadFilesToDatabase(dto, domain);
			}

			_context.RealEstates.Update(domain);
			await _context.SaveChangesAsync();

			return domain;
		}

		public async Task<RealEstate> Delete(Guid id)
		{
			var realEstate = await _context.RealEstates
				.FirstOrDefaultAsync(x => x.Id == id);

			var images = await _context.FileToDatabases
				.Where(x => x.RealEstateId == id)
				.Select(y => new FileToDatabaseDto
				{
					Id = y.Id,
					ImageTitle = y.ImageTitle,
					realEstateId = y.RealEstateId
				}).ToArrayAsync();

			await _fileServices.RemoveImagesFromDatabase(images);
			_context.RealEstates.Remove(realEstate);
			await _context.SaveChangesAsync();

			return realEstate;
		}

        public IEnumerable<RealEstate> GetAll()
        {
            return _context.RealEstates.ToList();
        }
    }
}
