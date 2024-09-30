using Microsoft.Extensions.Hosting;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Data;

namespace Shop.ApplicationServices.Services
{

    public class FileServices
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
            if(!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload"))
            {
                Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload");
            }

            foreach(var file in dto.Files)
            {
                string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "\\multipleFileUpload");
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
}
