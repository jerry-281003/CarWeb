using Microsoft.EntityFrameworkCore;

namespace CarWeb.Data
{
    public class CarWebContext : DbContext
    {
        public CarWebContext(DbContextOptions<CarWebContext> options)
            : base(options)
        {
        }

        public DbSet<CarWeb.Models.Customer> Customer { get; set; } = default!;

        public DbSet<CarWeb.Models.Feedback>? Feedback { get; set; }

        public DbSet<CarWeb.Models.Product>? Product { get; set; }
    }
}
