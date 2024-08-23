using Asp.Versioning;
using AutoMapper;
using CityInfo.Models;
using CityInfo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiVersion(1)]
	[ApiVersion(2)]
	public class CitiesController : ControllerBase
	{
		private readonly ICityInfoRepository _cityInfoRepository;
		private readonly IMapper _autoMapper;
		const int maxPageSize = 20;

		public CitiesController(ICityInfoRepository cityInfoRepository, IMapper autoMapper)
		{
			_cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
			_autoMapper = autoMapper ?? throw new ArgumentNullException(nameof(autoMapper));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CityWIthoutLandmarksDto>>> GetCities(
			string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
		{
			if (pageSize > maxPageSize)
			{
				pageSize = maxPageSize;
			}
			var (cities, paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
			Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
			return Ok(_autoMapper.Map<IEnumerable<CityWIthoutLandmarksDto>>(cities));
			//return Ok(_citiesDataStore.Cities);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCityById(int id, bool includeLandmarks = false)
		{
			var city = await _cityInfoRepository.GetCityAsync(id, includeLandmarks);
			if (city == null)
			{
				return NotFound();
			}
			if (includeLandmarks)
			{
				return Ok(_autoMapper.Map<CityDto>(city));
			}
			return Ok(_autoMapper.Map<CityWIthoutLandmarksDto>(city));
		}

	}
}