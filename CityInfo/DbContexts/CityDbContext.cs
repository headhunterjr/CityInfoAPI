using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DbContexts
{
	public class CityDbContext : DbContext
	{
		public CityDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<City> Cities { get; set; }
		public DbSet<Landmark> Landmarks { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>().HasData(
				new City("NYC")
				{
					Id = 1,
					Description = "pizzas, hot dogs, busy streets"
				},
				new City("Lviv")
				{
					Id = 2,
					Description = "buildings and that"
				});
			modelBuilder.Entity<Landmark>().HasData(
				new Landmark("NYC Square")
				{
					Id = 1,
					CityId = 1,
					Description = "Times square"
				},
				new Landmark("NYC street food restaurant")
				{
					Id = 2,
					CityId = 1,
					Description = "tacooooooooooos"
				},
				new Landmark("Lviv square")
				{
					Id = 3,
					CityId = 2,
					Description = "Market square"
				},
				new Landmark("LNU")
				{
					Id = 4,
					CityId = 2,
					Description = "best uni in the woooorrldddddd"
				}
				);
			base.OnModelCreating(modelBuilder);
		}
	}
}
