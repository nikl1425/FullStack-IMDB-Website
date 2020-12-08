using AutoMapper;
using DataService.Objects;
using WebService.ObjectDto;

namespace WebService.Models
{
    public class TopTenPosterProfile : Profile
    {
        public TopTenPosterProfile()
        {
            CreateMap<TopPoster, TopTenPosterDto>();
        }
    }
}