﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;


namespace Shop.ApplicationServices.Services
{
	public class FileServices : IFileServices
	{
		private readonly IHostEnvironment _webHost;
		private readonly ShopContext _context;

		public FileServices
			(
				IHostEnvironment webHost,
				ShopContext context
			)
		{
			_webHost = webHost;
			_context = context;
		}

		public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
		{
			if (dto.Files !=  null && dto.Files.Count > 0)
			{

			if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
			{
				Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
			}

			foreach (var file in dto.Files)
			{
				string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
				string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);

				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);

					FileToApi path = new FileToApi
					{
						Id = Guid.NewGuid(),
						ExistingFilePath = uniqueFileName,
						SpaceshipId = spaceship.Id
					};

					_context.FileToApis.AddAsync(path);
				}
			}
		}
			}

		public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
		{
			foreach (var dtosItem in dtos)
			{
				var imageId = await _context.FileToApis
					.FirstOrDefaultAsync(x => x.ExistingFilePath == dtosItem.ExistingFilePath);

				var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\"
					+ imageId.ExistingFilePath;

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				_context.FileToApis.Remove(imageId);
				await _context.SaveChangesAsync();
			}

			return null;
		}

		public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
		{

				var imageId = await _context.FileToApis
					.FirstOrDefaultAsync(x => x.Id == dto.Id);

				var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\"
					+ imageId.ExistingFilePath;

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}

				_context.FileToApis.Remove(imageId);
				await _context.SaveChangesAsync();

			return null;
		}

		public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
		{
			if (dto.Files != null && dto.Files.Count > 0)
			{
				foreach (var image in dto.Files)
				{
					using (var target = new MemoryStream())
					{
						FileToDatabase files = new FileToDatabase()
						{
							Id = Guid.NewGuid(),
							ImageTitle = image.FileName,
							RealEstateId = domain.Id
						};

						image.CopyTo(target);
						files.ImageData = target.ToArray();

						_context.FileToDatabases.Add(files);
					}
				}
			}
		}

		public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
		{
			var image = await _context.FileToDatabases
				.Where(x => x.Id == dto.Id)
				.FirstOrDefaultAsync();

			_context.FileToDatabases.Remove(image);
			await _context.SaveChangesAsync();

			return image;
		}

		public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
		{
			foreach (var dto in dtos)
			{
				var image = await _context.FileToDatabases
					.Where(x => x.Id == dto.Id)
					.FirstOrDefaultAsync();

				_context.FileToDatabases.Remove(image);
				await _context.SaveChangesAsync();
			}

			return null;
		}

        public void UploadKindergartenFilesToDatabase(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        KindergartenFileToDatabase files = new KindergartenFileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = image.FileName,
                            KindergartenId = domain.Id
                        };

                        image.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.KindergartenFileToDatabases.Add(files);
                    }
                }
            }
        }

        public async Task<KindergartenFileToDatabase> RemoveKindergartenImageFromDatabase(KindergartenFileToDatabaseDto dto)
        {
            var image = await _context.KindergartenFileToDatabases
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();

            _context.KindergartenFileToDatabases.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<KindergartenFileToDatabase> RemoveKindergartenImagesFromDatabase(KindergartenFileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var image = await _context.KindergartenFileToDatabases
                    .Where(x => x.Id == dto.Id)
                    .FirstOrDefaultAsync();

                _context.KindergartenFileToDatabases.Remove(image);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public List<FileToApi> GetFileToApis()
        {
            return _context.FileToApis.ToList();
        }

        public List<KindergartenFileToDatabase> GetKindergartenFiles()
        {
            return _context.KindergartenFileToDatabases.ToList();
        }

        public List<FileToDatabase> GetRealEstateFiles()
        {
            return _context.FileToDatabases.ToList();
        }
    }
}