using CityInfo.Entities;

namespace CityInfo.Services
{
	public interface ICityInfoRepository
	{
		Task<IEnumerable<City>> GetCitiesAsync();
		Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
		Task<City?> GetCityAsync(int cityId, bool includeLandmarks);
		Task<IEnumerable<Landmark>> GetLandmarksForCityAsync(int cityId);
		Task<Landmark?> GetLandmarkAsync(int cityId, int landmarkId);
		Task<bool> CityExistsAsync(int cityId);
		Task AddLandmarkForCityAsync(int cityId,  Landmark landmark);
		Task<bool> SaveChangesAsync();
		void DeleteLandmark(Landmark landmark);
	}
}
