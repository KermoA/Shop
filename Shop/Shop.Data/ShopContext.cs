using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;

namespace Shop.Data
{
    public class ShopContext : IdentityDbContext<ApplicationUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options)
        : base(options) { }


        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
        public DbSet<Kindergarten> Kindergartens { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
		public DbSet<FileToDatabase> FileToDatabases { get; set; }
        public DbSet<KindergartenFileToDatabase> KindergartenFileToDatabases { get; set; }
		public DbSet<IdentityRole> IdentityRoles { get; set; }


	}
}
