using Asp.Versioning;
using AutoMapper;
using CityInfo.Models;
using CityInfo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Controllers
{
	[Route("api/cities/{cityId}/[controller]")]
	[ApiVersion(2)]
	[ApiController]
	public class LandmarksController : ControllerBase
	{
		private readonly ILogger<LandmarksController> _logger;
		private readonly ICityInfoRepository _cityInfoRepository;
		private readonly IMapper _autoMapper;

		public LandmarksController(ILogger<LandmarksController> logger, ICityInfoRepository cityInfoRepository, IMapper autoMapper)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
			_autoMapper = autoMapper ?? throw new ArgumentNullException(nameof(autoMapper));
		}

		[HttpGet]
		public async Task<IActionResult> GetLandmarks(int cityId)
		{
			if (!await _cityInfoRepository.CityExistsAsync(cityId))
			{
				_logger.LogInformation($"City with ID {cityId} was not found when accessing landmarks");
				return NotFound();
			}
			var landmarks = await _cityInfoRepository.GetLandmarksForCityAsync(cityId);
			return Ok(_autoMapper.Map<IEnumerable<LandmarkDto>>(landmarks));
		}

		[HttpGet("{landmarkId}", Name = "GetLandmarkById")]
		public async Task<ActionResult<LandmarkDto>> GetLandmarkById(int cityId, int landmarkId)
		{
			if (!await _cityInfoRepository.CityExistsAsync(cityId))
			{
				_logger.LogInformation($"City with ID {cityId} was not found when accessing landmarks");
				return NotFound();
			}
			var landmark = await _cityInfoRepository.GetLandmarkAsync(cityId, landmarkId);
			if (landmark == null)
			{
				return NotFound();
			}
			return Ok(_autoMapper.Map<LandmarkDto>(landmark));
		}
		[HttpPost]
		public async Task<ActionResult<LandmarkDto>> CreateLandmark(int cityId, LandmarkCreateDto landmark)
		{
			if (!await _cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var resultLandmark = _autoMapper.Map<Entities.Landmark>(landmark);
			await _cityInfoRepository.AddLandmarkForCityAsync(cityId, resultLandmark);
			await _cityInfoRepository.SaveChangesAsync();
			var landmarkToReturn = _autoMapper.Map<LandmarkDto>(resultLandmark);
			return CreatedAtRoute("GetLandmark", new
			{
				cityId = cityId,
				landmarkId = landmarkToReturn.Id
			}, landmarkToReturn);
		}
		[HttpPut("{landmarkid}")]
		public async Task<IActionResult> UpdateLandmark(int cityId, int landmarkId, LandmarkUpdateDto landmark)
		{
			if (!await _cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var landmarkEntity = await _cityInfoRepository.GetLandmarkAsync(cityId, landmarkId);
			if (landmarkEntity == null)
			{
				return NotFound();
			}
			_autoMapper.Map(landmark, landmarkEntity);
			await _cityInfoRepository.SaveChangesAsync();
			return NoContent();
		}

		[HttpPatch("{landmarkid}")]
		public async Task<IActionResult> PatchLandmark(int cityId, int landmarkId, JsonPatchDocument<LandmarkUpdateDto> patchDocument)
		{
			if (!await _cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var landmarkEntity = await _cityInfoRepository.GetLandmarkAsync(cityId, landmarkId);
			if (landmarkEntity == null)
			{
				return NotFound();
			}
			var landmarkToPatch = _autoMapper.Map<LandmarkUpdateDto>(landmarkEntity);
			patchDocument.ApplyTo(landmarkToPatch, ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!TryValidateModel(landmarkToPatch))
			{
				return BadRequest(ModelState);
			}
			_autoMapper.Map(landmarkToPatch, landmarkEntity);
			await _cityInfoRepository.SaveChangesAsync();
			return NoContent();
		}
		[HttpDelete("{landmarkId}")]
		public async Task<IActionResult> DeleteLandmark(int cityId, int landmarkId)
		{
			if (!await _cityInfoRepository.CityExistsAsync(cityId))
			{
				return NotFound();
			}
			var landmarkEntity = await _cityInfoRepository.GetLandmarkAsync(landmarkId, cityId);
			if (landmarkEntity == null)
			{
				return NotFound();
			}
			_cityInfoRepository.DeleteLandmark(landmarkEntity);
			await _cityInfoRepository.SaveChangesAsync();
			return NoContent();
		}
	}
}
