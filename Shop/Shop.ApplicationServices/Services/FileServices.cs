﻿using Microsoft.EntityFrameworkCore;
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

		public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
		{
			foreach (var dto in dtos)
			{
				var imageId = await _context.FileToApis
					.FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);

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
	}
}