﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Kindergartens;
using Shop.Models.RealEstates;

namespace KindergartenProject.Controllers
{
	public class KindergartensController : Controller
	{
		private readonly ShopContext _context;
		private readonly IKindergartensServices _kindergartenServices;
        private readonly IFileServices _fileServices;

        public KindergartensController
			(
				ShopContext context,
				IKindergartensServices kindergartensServices,
                IFileServices fileServices
            )
		{
			_context = context;
			_kindergartenServices = kindergartensServices;
            _fileServices = fileServices;
        }
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Kindergartens
                .Select(x => new KindergartensIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    Teacher = x.Teacher
                });

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var kindergartens = query
                .OrderBy(k => k.KindergartenName) // Adjust ordering as necessary
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new KindergartensPagedViewModel
            {
                Kindergartens = kindergartens,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            return View(model);
        }

        [HttpGet]
		public IActionResult Create()
		{
			KindergartenCreateUpdateViewModel result = new();

			return View("CreateUpdate", result);
		}

		[HttpPost]
		public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
		{
			var dto = new KindergartenDto()
			{
				Id = vm.Id,
				GroupName = vm.GroupName,
				ChildrenCount = vm.ChildrenCount,
				KindergartenName = vm.KindergartenName,
				Teacher = vm.Teacher,
				CreatedAt = vm.CreatedAt,
				UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new KindergartenFileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KindergartenId = x.KindergartenId
                    }).ToArray()
            };

			var result = await _kindergartenServices.Create(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var kindergarten = await _kindergartenServices.DetailAsync(id);

			if (kindergarten == null)
			{
				return NotFound();
			}

            var photos = await _context.KindergartenFileToDatabases
                .Where(x => x.KindergartenId == id)
                .Select(y => new KindergartenImageViewModel
                {
                    KindergartenId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new KindergartenDetailsViewModel();

			vm.Id = kindergarten.Id;
			vm.GroupName = kindergarten.GroupName;
			vm.ChildrenCount = kindergarten.ChildrenCount;
			vm.KindergartenName = kindergarten.KindergartenName;
			vm.Teacher = kindergarten.Teacher;
			vm.CreatedAt = kindergarten.CreatedAt;
			vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(photos);

            return View(vm);

		}

		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var kindergarten = await _kindergartenServices.DetailAsync(id);

			if (kindergarten == null)
			{
				return NotFound();
			}

            var photos = await _context.KindergartenFileToDatabases
				.Where(x => x.KindergartenId == id)
				.Select(y => new KindergartenImageViewModel
				{
					KindergartenId = y.Id,
					ImageId = y.Id,
					ImageData = y.ImageData,
					ImageTitle = y.ImageTitle,
					Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
				}).ToArrayAsync();

            var vm = new KindergartenCreateUpdateViewModel();

			vm.Id = kindergarten.Id;
			vm.GroupName = kindergarten.GroupName;
			vm.ChildrenCount = kindergarten.ChildrenCount;
			vm.KindergartenName = kindergarten.KindergartenName;
			vm.Teacher = kindergarten.Teacher;
			vm.CreatedAt = kindergarten.CreatedAt;
			vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
		}

		[HttpPost]
		public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
		{
			var dto = new KindergartenDto()
			{
				Id = vm.Id,
				GroupName = vm.GroupName,
				ChildrenCount = vm.ChildrenCount,
				KindergartenName = vm.KindergartenName,
				Teacher = vm.Teacher,
				CreatedAt = vm.CreatedAt,
				UpdatedAt = vm.UpdatedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new KindergartenFileToDatabaseDto
                    {
                        Id = x.ImageId,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        KindergartenId = x.KindergartenId,
                    }).ToArray()
            };

			var result = await _kindergartenServices.Update(dto);

			if (result == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var kindergarten = await _kindergartenServices.DetailAsync(id);

			if (kindergarten == null)
			{
				return NotFound();
			}

            var photos = await _context.KindergartenFileToDatabases
               .Where(x => x.KindergartenId == id)
               .Select(y => new KindergartenImageViewModel
               {
                   KindergartenId = y.Id,
                   ImageId = y.Id,
                   ImageData = y.ImageData,
                   ImageTitle = y.ImageTitle,
                   Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
               }).ToArrayAsync();

            var vm = new KindergartenDeleteViewModel();

			vm.Id = id;
			vm.GroupName = kindergarten.GroupName;
			vm.ChildrenCount = kindergarten.ChildrenCount;
			vm.KindergartenName = kindergarten.KindergartenName;
			vm.Teacher = kindergarten.Teacher;
			vm.CreatedAt = kindergarten.CreatedAt;
			vm.UpdatedAt = kindergarten.UpdatedAt;
            vm.Image.AddRange(photos);

            return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmation(Guid id)
		{
			var kindergarten = await _kindergartenServices.Delete(id);

			if (kindergarten == null)
			{
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

        [HttpPost]
        public async Task<IActionResult> RemoveKindergartenImage([FromBody] KindergartenImageViewModel vm)
        {
            if (vm.ImageId == Guid.Empty)
            {
                return BadRequest("Invalid Image ID.");
            }

            var dto = new KindergartenFileToDatabaseDto { Id = vm.ImageId };
            var result = await _fileServices.RemoveKindergartenImageFromDatabase(dto);

            if (result != null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}