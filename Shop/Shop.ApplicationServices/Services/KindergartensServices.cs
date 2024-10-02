using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;

namespace Shop.ApplicationServices.Services
{
	public class KindergartensServices : IKindergartensServices
	{
		private readonly ShopContext _context;

		public KindergartensServices
			(
				ShopContext context
			)
		{
			_context = context;
		}

		public async Task<Kindergarten> Create(KindergartenDto dto)
		{
			Kindergarten kindergarten = new Kindergarten();

			kindergarten.Id = Guid.NewGuid();
			kindergarten.GroupName = dto.GroupName;
			kindergarten.ChildrenCount = dto.ChildrenCount;
			kindergarten.KindergartenName = dto.KindergartenName;
			kindergarten.Teacher = dto.Teacher;
			kindergarten.CreatedAt = DateTime.Now;
			kindergarten.UpdatedAt = DateTime.Now;

			await _context.Kindergartens.AddAsync(kindergarten);
			await _context.SaveChangesAsync();

			return kindergarten;
		}

		public async Task<Kindergarten> DetailAsync(Guid id)
		{
			var result = await _context.Kindergartens
				.FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async Task<Kindergarten> Update(KindergartenDto dto)
		{
			Kindergarten domain = new();

			domain.Id = dto.Id;
			domain.GroupName = dto.GroupName;
			domain.ChildrenCount = dto.ChildrenCount;
			domain.KindergartenName = dto.KindergartenName;
			domain.Teacher = dto.Teacher;
			domain.CreatedAt = dto.CreatedAt;
			domain.UpdatedAt = DateTime.Now;

			_context.Kindergartens.Update(domain);
			await _context.SaveChangesAsync();

			return domain;
		}

		public async Task<Kindergarten> Delete(Guid id)
		{
			var kindergarten = await _context.Kindergartens
				.FirstOrDefaultAsync(x => x.Id == id);

			_context.Kindergartens.Remove(kindergarten);
			await _context.SaveChangesAsync();

			return kindergarten;
		}
	}
}