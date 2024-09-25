using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;

namespace Shop.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
        : base(options) { }


        public DbSet<Spaceship> Spaceships { get; set; }


    }
}
