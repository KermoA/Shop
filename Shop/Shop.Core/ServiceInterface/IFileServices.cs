using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceship spaceship);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
		void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain);
		Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
		Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos);
		Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);

	}
}
