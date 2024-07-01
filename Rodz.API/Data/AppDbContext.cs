using Microsoft.EntityFrameworkCore;
using DES.Domain.Entities;


namespace Rodz.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dessert> Dessert { get; set; }
        public DbSet<Category> Categories { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



    }
}
