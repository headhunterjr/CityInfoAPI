using System.ComponentModel.DataAnnotations;

namespace CityInfo.Models
{
	public class LandmarkCreateDto
	{
		[Required(ErrorMessage = "Please provide the name")]
		[MaxLength(50)]
		public string Name { get; set; } = string.Empty;
		[MaxLength(200)]
		public string? Description { get; set; }
	}
}
