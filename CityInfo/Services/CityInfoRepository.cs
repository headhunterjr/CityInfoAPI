using CityInfo.DbContexts;
using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Services
{
	public class CityInfoRepository : ICityInfoRepository
	{
		private readonly CityDbContext _cityDbContext;

		public CityInfoRepository(CityDbContext cityDbContext)
		{
			_cityDbContext = cityDbContext ?? throw new ArgumentNullException(nameof(cityDbContext));
		}

		public async Task<IEnumerable<City>> GetCitiesAsync()
		{
			return await _cityDbContext.Cities.OrderBy(c => c.Name).ToListAsync();
		}
		public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
		{
			var collection = _cityDbContext.Cities as IQueryable<City>;
			if (!string.IsNullOrWhiteSpace(name))
			{
				name = name.Trim();
				collection = collection.Where(c => c.Name == name);
			}
			if (!string.IsNullOrWhiteSpace(searchQuery))
			{
				searchQuery = searchQuery.Trim().ToLower();
				collection = collection.Where(c => c.Name.ToLower().Contains(searchQuery)
				|| (c.Description != null && c.Description.ToLower().Contains(searchQuery)));
			}
			var totalItemCount = await collection.CountAsync();
			PaginationMetadata paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);
			var collectionToReturn = await collection.OrderBy(c => c.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();
			return (collectionToReturn, paginationMetadata);
		}

		public async Task<City?> GetCityAsync(int cityId, bool includeLandmarks)
		{
			if (includeLandmarks)
			{
				return await _cityDbContext.Cities.Include(c => c.Landmarks).Where(c => c.Id == cityId).FirstOrDefaultAsync();
			}
			else
			{
				return await _cityDbContext.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
			}
		}
		public async Task<IEnumerable<Landmark>> GetLandmarksForCityAsync(int cityId)
		{
			return await _cityDbContext.Landmarks.Where(l => l.CityId == cityId).ToListAsync();
		}

		public async Task<Landmark?> GetLandmarkAsync(int cityId, int landmarkId)
		{
			return await _cityDbContext.Landmarks.Where(l => l.CityId  == cityId && l.Id == landmarkId).FirstOrDefaultAsync();
		}
		public async Task<bool> CityExistsAsync(int cityId)
		{
			return await _cityDbContext.Cities.AnyAsync(c => c.Id == cityId);
		}
		public async Task AddLandmarkForCityAsync(int cityId, Landmark landmark)
		{
			var city = await GetCityAsync(cityId, false);
			if (city != null)
			{
				city.Landmarks.Add(landmark);
			}
		}
		public async Task<bool> SaveChangesAsync()
		{
			return await _cityDbContext.SaveChangesAsync() >= 0;
		}

		public void DeleteLandmark(Landmark landmark)
		{
			_cityDbContext.Remove(landmark);
		}
	}
}
