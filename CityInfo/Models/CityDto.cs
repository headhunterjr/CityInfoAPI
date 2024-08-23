namespace CityInfo.Models
{
	public class CityDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public int NumberOfLandmarks
		{
			get
			{
				return Landmarks.Count;
			}
		}
		public ICollection<LandmarkDto> Landmarks { get; set; } = new List<LandmarkDto>();
	}
}
