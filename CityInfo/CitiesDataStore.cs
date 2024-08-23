using CityInfo.Models;

namespace CityInfo
{
	public class CitiesDataStore
	{
		public List<CityDto> Cities { get; set; }
		//public static CitiesDataStore Current { get; } = new CitiesDataStore();
		public CitiesDataStore()
		{
			Cities = new List<CityDto>()
			{
				new CityDto()
				{
					Id = 1,
					Name = "NYC",
					Description = "pizzas, hot dogs, busy streets",
					Landmarks = new List<LandmarkDto>()
					{
						new LandmarkDto()
						{
							Id = 1,
							Name = "Square",
							Description = "Times square"
						},
						new LandmarkDto()
						{
							Id = 2,
							Name = "Restaurant",
							Description = "Tacoooooooos"
						}
					}
				},
				new CityDto()
				{
					Id = 2,
					Name = "Lviv",
					Description = "buildings and that",
					Landmarks = new List<LandmarkDto>()
					{
						new LandmarkDto()
						{
							Id = 3,
							Name = "Square",
							Description = "Market square"
						},
						new LandmarkDto()
						{
							Id = 4,
							Name = "LNU",
							Description = "best uni in the woooorrlddd"
						}
					}
				}
			};
		}
	}
}
