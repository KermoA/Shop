﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
    public class SpaceshipsController : Controller
	{
		private readonly ShopContext _context;
		private readonly ISpaceshipsServices _spaceshipServices;
		private readonly IFileServices _fileServices;

		public SpaceshipsController
			(
				ShopContext context,
				ISpaceshipsServices spaceshipsServices,
				IFileServices fileServices
			)
		{
			_context = context;
			_spaceshipServices = spaceshipsServices;
			_fileServices = fileServices;
		}


        public IActionResult Index(int pageNumber = 1, int pageSize = 5, string sortOrder = null)
        {
            ViewData["Title"] = "Spaceships";
            var query = _context.Spaceships
                .Select(x => new SpaceshipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Typename = x.Typename,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew,
					CreatedAt = x.CreatedAt,
                });

            switch (sortOrder)
            {
				case "name":
                    query = query.OrderBy(s => s.Name);
					break;
                case "name_desc":
                    query = query.OrderByDescending(s => s.Name);
                    break;
                case "type":
                    query = query.OrderBy(s => s.Typename);
                    break;
                case "type_desc":
                    query = query.OrderByDescending(s => s.Typename);
                    break;
                case "date":
                    query = query.OrderBy(s => s.BuiltDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(s => s.BuiltDate);
                    break;
                case "crew":
                    query = query.OrderBy(s => s.Crew);
                    break;
                case "crew_desc":
                    query = query.OrderByDescending(s => s.Crew);
                    break;
				case "created_at":
					query = query.OrderBy(s => s.CreatedAt);
					break;
                default:
                    query = query.OrderByDescending(s => s.CreatedAt);
                    break;
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var spaceships = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new SpaceshipsPagedViewModel
            {
                Spaceships = spaceships,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize,
                SortOrder = sortOrder
            };

            return View(model);
        }

        [HttpGet]
		public IActionResult Create()
		{
            ViewData["Title"] = "Create Spaceship";
            SpaceshipCreateUpdateViewModel result = new();

			return View("CreateUpdate", result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
		{
			var dto = new SpaceshipDto()
			{
				Id = vm.Id,
				Name = vm.Name,
				Typename = vm.Typename,
				SpaceshipModel = vm.SpaceshipModel,
				BuiltDate = vm.BuiltDate,
				Crew = vm.Crew,
				EnginePower = vm.EnginePower,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt,
				Files = vm.Files,
				FileToApiDtos = vm.Image
					.Select(x => new FileToApiDto
					{
						Id = x.ImageId,
						ExistingFilePath = x.FilePath,
						SpaceshipId = x.SpaceshipId
					}).ToArray()
			};

			var result = await _spaceshipServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
            ViewData["Title"] = "Spaceship Details";
            var spaceship = await _spaceshipServices.DetailAsync(id);

			if (spaceship == null)
			{
				return View("Error");
			}

			var images = await _context.FileToApis
				.Where(x => x.SpaceshipId == id)
				.Select(y => new ImageViewModel
				{
					FilePath = y.ExistingFilePath,
					ImageId = y.Id
				}).ToArrayAsync();

			var vm = new SpaceshipDetailsViewModel();

			vm.Id = spaceship.Id;
			vm.Name = spaceship.Name;
			vm.Typename = spaceship.Typename;
			vm.BuiltDate = spaceship.BuiltDate;
			vm.SpaceshipModel = spaceship.SpaceshipModel;
			vm.Crew = spaceship.Crew;
			vm.EnginePower = spaceship.EnginePower;
			vm.CreatedAt = spaceship.CreatedAt;
			vm.ModifiedAt = spaceship.ModifiedAt;
			vm.Images.AddRange(images);

			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
            ViewData["Title"] = "Update Spaceship";

            var spaceship = await _spaceshipServices.DetailAsync(id);

			if (spaceship == null)
			{
				return NotFound();
			}

			var images = await _context.FileToApis
				.Where(x => x.SpaceshipId == id)
				.Select(y => new ImageViewModel
				{
					FilePath = y.ExistingFilePath,
					ImageId = y.Id
				}).ToArrayAsync();

			var vm = new SpaceshipCreateUpdateViewModel();

			vm.Id = spaceship.Id;
			vm.Name = spaceship.Name;
			vm.Typename = spaceship.Typename;
			vm.BuiltDate = spaceship.BuiltDate;
			vm.SpaceshipModel = spaceship.SpaceshipModel;
			vm.Crew = spaceship.Crew;
			vm.EnginePower = spaceship.EnginePower;
			vm.CreatedAt = spaceship.CreatedAt;
			vm.ModifiedAt = spaceship.ModifiedAt;
			vm.Image.AddRange(images);

			return View("CreateUpdate", vm);
		}

		[HttpPost]
		public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
		{
			var dto = new SpaceshipDto()
			{
				Id = vm.Id,
				Name = vm.Name,
				Typename = vm.Typename,
				SpaceshipModel = vm.SpaceshipModel,
				BuiltDate = vm.BuiltDate,
				Crew = vm.Crew,
				EnginePower = vm.EnginePower,
				CreatedAt = vm.CreatedAt,
				ModifiedAt = vm.ModifiedAt,
				Files = vm.Files,
				FileToApiDtos = vm.Image
					.Select(x => new FileToApiDto
					{
						Id = x.ImageId,
						ExistingFilePath = x.FilePath,
						SpaceshipId = x.SpaceshipId
					}).ToArray()
			};

			var result = await _spaceshipServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index), vm);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
            ViewData["Title"] = "Delete Spaceship";
            var spaceship = await _spaceshipServices.DetailAsync(id);

			if (spaceship == null)
			{
				return NotFound();
			}

			var images = await _context.FileToApis
				.Where(x => x.SpaceshipId == id)
				.Select(y => new ImageViewModel
				{
					FilePath = y.ExistingFilePath,
					ImageId = y.Id
				}).ToArrayAsync();

			var vm = new SpaceshipDeleteViewModel();

			vm.Id = spaceship.Id;
			vm.Name = spaceship.Name;
			vm.Typename = spaceship.Typename;
			vm.BuiltDate = spaceship.BuiltDate;
			vm.SpaceshipModel = spaceship.SpaceshipModel;
			vm.Crew = spaceship.Crew;
			vm.EnginePower = spaceship.EnginePower;
			vm.CreatedAt = spaceship.CreatedAt;
			vm.ModifiedAt = spaceship.ModifiedAt;
			vm.ImageViewModel.AddRange(images);

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var spaceship = await _spaceshipServices.Delete(id);

			if (spaceship == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> RemoveImage(ImageViewModel vm)
		{
			var dto = new FileToApiDto()			
			{
				Id = vm.ImageId
			};

			var image = await _fileServices.RemoveImageFromApi(dto);
			if (image == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}
	}
}