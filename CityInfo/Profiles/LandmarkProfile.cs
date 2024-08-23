using AutoMapper;
using CityInfo.Models;

namespace CityInfo.Profiles
{
	public class LandmarkProfile : Profile
	{
		public LandmarkProfile()
		{
			CreateMap<Entities.Landmark, LandmarkDto>();
			CreateMap<LandmarkCreateDto, Entities.Landmark>();
			CreateMap<LandmarkUpdateDto, Entities.Landmark>();
			CreateMap<Entities.Landmark, LandmarkUpdateDto>();
		}
	}
}
