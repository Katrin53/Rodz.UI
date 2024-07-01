using DES.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rodz.UI.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Dessert> Dessert { get; set; }
		public DbSet<Category> Categories { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	}
}
